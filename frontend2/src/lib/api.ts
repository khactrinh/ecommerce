// import { API_URL } from "@/config";

// export async function fetcher<T>(url: string): Promise<T> {
//   const controller = new AbortController();

//   const timeout = setTimeout(() => controller.abort(), 5000);

//   try {
//     const res = await fetch(`${API_URL}${url}`, {
//       cache: "no-store",
//       signal: controller.signal,
//     });

//     if (!res.ok) {
//       const errorText = await res.text();
//       throw new Error(`API ${res.status}: ${errorText}`);
//     }

//     return res.json();
//   } catch (err) {
//     console.error("FETCH ERROR:", err);
//     throw err;
//   } finally {
//     clearTimeout(timeout);
//   }
// }

import { HttpError } from "./httpError";
import { getAccessToken } from "./auth";
import { ApiError } from "@/types/api";

const API_URL = process.env.NEXT_PUBLIC_API_URL || "http://127.0.0.1:5001";

type FetchOptions = RequestInit & {
  timeout?: number;
  retry?: number;
};

// ✅ type guard để đọc message an toàn
function isErrorResponse(data: unknown): data is { message?: string } {
  return typeof data === "object" && data !== null && "message" in data;
}

export async function fetcher<T>(
  url: string,
  options: FetchOptions = {},
): Promise<T> {
  const { timeout = 5000, retry = 0, headers, ...rest } = options;

  const controller = new AbortController();
  const id = setTimeout(() => controller.abort(), timeout);

  //const token = getAccessToken();

  try {
    const res = await fetch(`${API_URL}${url}`, {
      ...rest,
      cache: "no-store",
      headers: {
        "Content-Type": "application/json",
        //...(token && { Authorization: `Bearer ${token}` }),
        ...headers,
      },
      signal: controller.signal,
    });

    const contentType = res.headers.get("content-type");

    // ✅ bỏ = null → tránh confusion type
    let data: unknown;

    if (contentType?.includes("application/json")) {
      data = await res.json();
    } else {
      data = await res.text();
    }

    // ✅ xử lý error chuẩn hơn (không dùng any)
    if (!res.ok) {
      const message = isErrorResponse(data)
        ? data.message || "API error"
        : typeof data === "string"
          ? data
          : "API error";

      throw new HttpError(res.status, message, data);
    }

    return data as T;
  } catch (err: unknown) {
    // ✅ timeout
    if (err instanceof Error && err.name === "AbortError") {
      throw new HttpError(408, "Request timeout");
    }

    console.error("Fetch error:", err);

    throw err;
  } finally {
    clearTimeout(id);
  }
}
