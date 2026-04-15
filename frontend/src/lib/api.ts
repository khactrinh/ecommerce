import { ApiResponse, LoginResponse } from "./types";

const BASE_URL = process.env.NEXT_PUBLIC_API_URL || "http://localhost:5204";

class ApiClient {
  private refreshPromise: Promise<boolean> | null = null;

  // ======================
  // TOKEN HANDLING
  // ======================
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

  // ======================
  // CORE REQUEST
  // ======================
  private async request<T>(
    endpoint: string,
    options: RequestInit = {},
    isRetry = false,
  ): Promise<T> {
    const token = this.getToken();

    const headers = new Headers(options.headers || {});
    headers.set("accept", "*/*");

    // chỉ set JSON nếu không phải FormData
    if (!(options.body instanceof FormData)) {
      headers.set("Content-Type", "application/json");
    }

    if (token) {
      headers.set("Authorization", `Bearer ${token}`);
    }

    // timeout
    const controller = new AbortController();
    const timeout = setTimeout(() => controller.abort(), 10000);

    try {
      const response = await fetch(`${BASE_URL}${endpoint}`, {
        ...options,
        headers,
        signal: controller.signal,
      });

      // ======================
      // HANDLE 401 + REFRESH
      // ======================
      if (response.status === 401 && !isRetry) {
        const refreshed = await this.refreshToken();

        if (refreshed) {
          return this.request<T>(endpoint, options, true);
        }

        throw new ApiError(401, { message: "Unauthorized" });
      }

      // ======================
      // HANDLE ERROR
      // ======================
      if (!response.ok) {
        let errorData: any = {};

        try {
          errorData = await response.json();
        } catch {}

        throw new ApiError(response.status, errorData);
      }

      return await response.json();
    } finally {
      clearTimeout(timeout);
    }
  }

  // ======================
  // REFRESH TOKEN (LOCKED)
  // ======================
  private async refreshToken(): Promise<boolean> {
    if (this.refreshPromise) return this.refreshPromise;

    this.refreshPromise = (async () => {
      const refreshToken = this.getRefreshToken();
      if (!refreshToken) return false;

      try {
        const resp = await fetch(`${BASE_URL}/api/v1/auth/refresh`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            accept: "*/*",
          },
          body: JSON.stringify({ token: refreshToken }),
        });

        if (!resp.ok) return false;

        const data: LoginResponse = await resp.json();

        this.setToken(data.accessToken);
        this.setRefreshToken(data.refreshToken);

        return true;
      } catch (err) {
        console.error("Refresh token failed", err);
        return false;
      } finally {
        this.refreshPromise = null;
      }
    })();

    return this.refreshPromise;
  }

  // ======================
  // METHODS
  // ======================
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

// ======================
// CUSTOM ERROR CLASS
// ======================
export class ApiError extends Error {
  constructor(
    public status: number,
    public data: any,
  ) {
    super(data?.message || `API Error ${status}`);
  }
}

export const api = new ApiClient();
