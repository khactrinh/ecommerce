import { API_URL } from "@/config";

export async function fetcher<T>(url: string): Promise<T> {
  const res = await fetch(`${API_URL}${url}`, {
    cache: "no-store",
  });

  if (!res.ok) {
    throw new Error("API error");
  }

  return res.json();
}
