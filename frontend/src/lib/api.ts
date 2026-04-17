import type { LoginResponse } from "./types";

const BASE_URL = process.env.NEXT_PUBLIC_API_URL || "http://localhost:5204";

export class ApiError extends Error {
  constructor(
      public status: number,
      public data: any,
  ) {
    super(data?.message || `API Error ${status}`);
  }
}

class ApiClient {
  private refreshPromise: Promise<boolean> | null = null;

  private getToken() {
    if (typeof window === "undefined") return null;
    return localStorage.getItem("auth_token");
  }

  private setToken(token: string) {
    if (typeof window === "undefined") return;
    localStorage.setItem("auth_token", token);
  }

  private getRefreshToken() {
    if (typeof window === "undefined") return null;
    return localStorage.getItem("refresh_token");
  }

  private setRefreshToken(token: string) {
    if (typeof window === "undefined") return;
    localStorage.setItem("refresh_token", token);
  }

  private async request<T>(
      endpoint: string,
      options: RequestInit = {},
      isRetry = false,
  ): Promise<T> {
    const token = this.getToken();

    const isAuthEndpoint =
        endpoint.includes("/api/v1/auth/login") ||
        endpoint.includes("/api/v1/auth/refresh");

    const headers = new Headers(options.headers || {});
    headers.set("accept", "*/*");

    if (!(options.body instanceof FormData)) {
      headers.set("Content-Type", "application/json");
    }

    if (token && !isAuthEndpoint) {
      headers.set("Authorization", `Bearer ${token}`);
    }

    const controller = new AbortController();
    const timeout = setTimeout(() => controller.abort(), 15000);

    try {
      const response = await fetch(`${BASE_URL}${endpoint}`, {
        ...options,
        headers,
        signal: controller.signal,
      });

      // 🔥 Auto refresh token khi 401
      if (response.status === 401 && !isRetry) {
        const ok = await this.refreshToken();

        if (ok) return this.request<T>(endpoint, options, true);

        // hết hạn login → clear
        console.warn("Session expired. Clearing auth data.");
        if (typeof window !== "undefined") {
          localStorage.removeItem("auth_token");
          localStorage.removeItem("refresh_token");
          window.dispatchEvent(new Event("auth_expired"));
        }

        throw new ApiError(401, { message: "Unauthorized" });
      }

      // ❌ HTTP error
      if (!response.ok) {
        let errorData: any = {};
        try {
          errorData = await response.json();
        } catch {}
        throw new ApiError(response.status, errorData);
      }

      // ✅ parse response
      const text = await response.text();
      if (!text) return null as T;

      let json: any;
      try {
        json = JSON.parse(text);
      } catch {
        throw new ApiError(response.status, {
          message: "Invalid JSON response",
        });
      }

      // 🔥🔥 QUAN TRỌNG NHẤT: handle ApiResponse<T>
      if (typeof json === "object" && "success" in json) {
        if (!json.success) {
          throw new ApiError(response.status, json);
        }

        return json.data as T; // ✅ luôn unwrap
      }

      // fallback (login / refresh / legacy)
      return json as T;
    } finally {
      clearTimeout(timeout);
    }
  }

  private async refreshToken(): Promise<boolean> {
    if (this.refreshPromise) return this.refreshPromise;

    this.refreshPromise = (async () => {
      const refreshToken = this.getRefreshToken();
      if (!refreshToken) return false;

      try {
        console.log("Attempting to refresh token...");

        const resp = await fetch(`${BASE_URL}/api/v1/auth/refresh`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            accept: "*/*",
          },
          body: JSON.stringify({
            token: refreshToken,
            Token: refreshToken,
            ipAddress: "",
          }),
        });

        if (!resp.ok) {
          console.error("Refresh token request failed", resp.status);
          return false;
        }

        const json = await resp.json();
        const data = json.data || json;

        const newAccessToken = data.accessToken || data.AccessToken;
        const newRefreshToken = data.refreshToken || data.RefreshToken;

        if (!newAccessToken) {
          console.error("No access token in refresh response", data);
          return false;
        }

        this.setToken(newAccessToken);

        if (newRefreshToken) {
          this.setRefreshToken(newRefreshToken);
        }

        console.log("Token refreshed successfully");
        return true;
      } catch (err) {
        console.error("Refresh token error:", err);
        return false;
      } finally {
        this.refreshPromise = null;
      }
    })();

    return this.refreshPromise;
  }

  get<T>(endpoint: string, options?: RequestInit) {
    return this.request<T>(endpoint, {
      ...options,
      method: "GET",
    });
  }

  post<T, B = unknown>(endpoint: string, body: B, options?: RequestInit) {
    return this.request<T>(endpoint, {
      ...options,
      method: "POST",
      body: JSON.stringify(body),
    });
  }
}

export const api = new ApiClient();