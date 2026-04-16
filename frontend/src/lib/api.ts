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

      // refresh token
      if (response.status === 401 && !isRetry) {
        const ok = await this.refreshToken();
        if (ok) return this.request<T>(endpoint, options, true);
        throw new ApiError(401, { message: "Unauthorized" });
      }

      // error
      if (!response.ok) {
        let errorData: any = {};
        try {
          errorData = await response.json();
        } catch {}
        throw new ApiError(response.status, errorData);
      }

      // ✅ FIX: handle empty response
      const text = await response.text();
      if (!text) return null as T;

      const json = JSON.parse(text);

      // nếu có wrapper ApiResponse
      if (json?.data !== undefined) {
        return json.data as T;
      }

      // nếu raw response (login, refresh cũ)
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
