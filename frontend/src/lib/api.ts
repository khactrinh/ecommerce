// import type { LoginResponse } from "./types";
//
// const BASE_URL = process.env.NEXT_PUBLIC_API_URL || "http://localhost:5204";
//
// export class ApiError extends Error {
//   constructor(
//       public status: number,
//       public data: any,
//   ) {
//     super(data?.message || data?.Message || `API Error ${status}`);
//   }
// }
//
// class ApiClient {
//   private refreshPromise: Promise<boolean> | null = null;
//
//   // ================= TOKEN =================
//   private getToken() {
//     if (typeof window === "undefined") return null;
//     return localStorage.getItem("auth_token");
//   }
//
//   private setToken(token: string) {
//     if (typeof window === "undefined") return;
//     localStorage.setItem("auth_token", token);
//   }
//
//   private getRefreshToken() {
//     if (typeof window === "undefined") return null;
//     return localStorage.getItem("refresh_token");
//   }
//
//   private setRefreshToken(token: string) {
//     if (typeof window === "undefined") return;
//     localStorage.setItem("refresh_token", token);
//   }
//
//   // ================= CORE REQUEST =================
//   private async request<T>(
//       endpoint: string,
//       options: RequestInit = {},
//       isRetry = false,
//   ): Promise<T> {
//     const token = this.getToken();
//
//     const isAuthEndpoint =
//         endpoint.includes("/api/v1/auth/login") ||
//         endpoint.includes("/api/v1/auth/refresh");
//
//     const headers = new Headers(options.headers || {});
//     headers.set("accept", "*/*");
//
//     if (!(options.body instanceof FormData)) {
//       headers.set("Content-Type", "application/json");
//     }
//
//     if (token && !isAuthEndpoint) {
//       headers.set("Authorization", `Bearer ${token}`);
//     }
//
//     const controller = new AbortController();
//     const timeout = setTimeout(() => controller.abort(), 15000);
//
//     try {
//       const response = await fetch(`${BASE_URL}${endpoint}`, {
//         ...options,
//         headers,
//         signal: controller.signal,
//       });
//
//       // ================= AUTO REFRESH =================
//       if (response.status === 401 && !isRetry) {
//         const ok = await this.refreshToken();
//
//         if (ok) return this.request<T>(endpoint, options, true);
//
//         console.warn("Session expired. Clearing auth data.");
//
//         if (typeof window !== "undefined") {
//           localStorage.removeItem("auth_token");
//           localStorage.removeItem("refresh_token");
//           window.dispatchEvent(new Event("auth_expired"));
//         }
//
//         throw new ApiError(401, { message: "Unauthorized" });
//       }
//
//       // ================= HTTP ERROR =================
//       if (!response.ok) {
//         let errorData: any = {};
//         try {
//           errorData = await response.json();
//         } catch {}
//
//         throw new ApiError(response.status, errorData);
//       }
//
//       // ================= PARSE =================
//       const text = await response.text();
//       if (!text) return null as T;
//
//       let json: any;
//       try {
//         json = JSON.parse(text);
//       } catch {
//         throw new ApiError(response.status, {
//           message: "Invalid JSON response",
//         });
//       }
//
//       // ================= 🔥 API RESPONSE UNWRAP =================
//       // support BOTH camelCase + PascalCase
//       const isApiResponse =
//           typeof json === "object" &&
//           (("success" in json) || ("Success" in json));
//
//       if (isApiResponse) {
//         const success = json.success ?? json.Success;
//
//         if (!success) {
//           throw new ApiError(response.status, json);
//         }
//
//         // unwrap data (camelCase hoặc PascalCase)
//         const data = json.data ?? json.Data;
//
//         return data as T;
//       }
//
//       // ================= FALLBACK =================
//       return json as T;
//     } finally {
//       clearTimeout(timeout);
//     }
//   }
//
//   // ================= REFRESH TOKEN =================
//   private async refreshToken(): Promise<boolean> {
//     if (this.refreshPromise) return this.refreshPromise;
//
//     this.refreshPromise = (async () => {
//       const refreshToken = this.getRefreshToken();
//       if (!refreshToken) return false;
//
//       try {
//         console.log("Attempting to refresh token...");
//
//         const resp = await fetch(`${BASE_URL}/api/v1/auth/refresh`, {
//           method: "POST",
//           headers: {
//             "Content-Type": "application/json",
//             accept: "*/*",
//           },
//           body: JSON.stringify({
//             token: refreshToken,
//             Token: refreshToken, // fallback BE PascalCase
//             ipAddress: "",
//           }),
//         });
//
//         if (!resp.ok) {
//           console.error("Refresh token request failed", resp.status);
//           return false;
//         }
//
//         const json = await resp.json();
//
//         // support BOTH
//         const data = json.data ?? json.Data ?? json;
//
//         const newAccessToken =
//             data.accessToken ?? data.AccessToken;
//
//         const newRefreshToken =
//             data.refreshToken ?? data.RefreshToken;
//
//         if (!newAccessToken) {
//           console.error("No access token in refresh response", data);
//           return false;
//         }
//
//         this.setToken(newAccessToken);
//
//         if (newRefreshToken) {
//           this.setRefreshToken(newRefreshToken);
//         }
//
//         console.log("Token refreshed successfully");
//         return true;
//       } catch (err) {
//         console.error("Refresh token error:", err);
//         return false;
//       } finally {
//         this.refreshPromise = null;
//       }
//     })();
//
//     return this.refreshPromise;
//   }
//
//   // ================= METHODS =================
//   get<T>(endpoint: string, options?: RequestInit) {
//     return this.request<T>(endpoint, {
//       ...options,
//       method: "GET",
//     });
//   }
//
//   post<T, B = unknown>(endpoint: string, body: B, options?: RequestInit) {
//     return this.request<T>(endpoint, {
//       ...options,
//       method: "POST",
//       body: JSON.stringify(body),
//     });
//   }
// }
//
// export const api = new ApiClient();

import type { ApiResponse } from "@/shared/types/api";

const BASE_URL = process.env.NEXT_PUBLIC_API_URL || "http://localhost:5204";

export class ApiError extends Error {
  constructor(
      public status: number,
      public data: any
  ) {
    super(data?.message || data?.Message || `API Error ${status}`);
  }
}

class ApiClient {
  private refreshPromise: Promise<boolean> | null = null;

  // ================= TOKEN =================
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

  // ================= CORE =================
  private async request<T>(
      endpoint: string,
      options: RequestInit = {},
      isRetry = false
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

      // ================= AUTO REFRESH =================
      if (response.status === 401 && !isRetry) {
        const ok = await this.refreshToken();

        if (ok) return this.request<T>(endpoint, options, true);

        console.warn("Session expired. Clearing auth data.");

        if (typeof window !== "undefined") {
          localStorage.removeItem("auth_token");
          localStorage.removeItem("refresh_token");
          window.dispatchEvent(new Event("auth_expired"));
        }

        throw new ApiError(401, { message: "Unauthorized" });
      }

      // ================= ERROR =================
      if (!response.ok) {
        let errorData: any = {};
        try {
          errorData = await response.json();
        } catch {}

        throw new ApiError(response.status, errorData);
      }

      // ================= PARSE =================
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

      // ================= 🔥 UNWRAP ApiResponse =================
      const isApiResponse =
          typeof json === "object" &&
          (("success" in json) || ("Success" in json));

      if (isApiResponse) {
        const success = json.success ?? json.Success;

        if (!success) {
          throw new ApiError(response.status, json);
        }

        const data = json.data ?? json.Data;

        return data as T;
      }

      return json as T;
    } finally {
      clearTimeout(timeout);
    }
  }

  // ================= REFRESH =================
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
          console.error("Refresh token failed", resp.status);
          return false;
        }

        const json = await resp.json();

        const data = json.data ?? json.Data ?? json;

        const newAccessToken =
            data.accessToken ?? data.AccessToken;

        const newRefreshToken =
            data.refreshToken ?? data.RefreshToken;

        if (!newAccessToken) return false;

        this.setToken(newAccessToken);

        if (newRefreshToken) {
          this.setRefreshToken(newRefreshToken);
        }

        console.log("Token refreshed");
        return true;
      } catch (err) {
        console.error("Refresh error:", err);
        return false;
      } finally {
        this.refreshPromise = null;
      }
    })();

    return this.refreshPromise;
  }

  // ================= METHODS =================
  get<T>(endpoint: string, options?: RequestInit) {
    return this.request<T>(endpoint, {
      ...options,
      method: "GET",
    });
  }

  post<T, B = unknown>(
      endpoint: string,
      body: B,
      options?: RequestInit
  ) {
    return this.request<T>(endpoint, {
      ...options,
      method: "POST",
      body: JSON.stringify(body),
    });
  }
}

export const api = new ApiClient();