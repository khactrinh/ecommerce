"use client";

import { api } from "@/lib/api";
import { useQuery } from "@tanstack/react-query";
// import { getProducts } from "@/services/productService";

// export function useProducts(page = 1, pageSize = 12) {
//   return useQuery({
//     queryKey: ["products", page, pageSize],
//     queryFn: () => getProducts(page, pageSize),

//     placeholderData: (prev) => prev, // 👈 thay cho keepPreviousData
//     staleTime: 1000 * 60,
//     retry: false,
//   });
// }

export function useProducts(
  page: number,
  pageSize: number,
  categoryId?: string,
) {
  const query = new URLSearchParams();

  query.append("page", page.toString());
  query.append("pageSize", pageSize.toString());

  if (categoryId) {
    query.append("categoryId", categoryId); // ✅ FIX HERE
  }

  return useQuery({
    queryKey: ["products", page, categoryId],
    queryFn: async () => {
      const res = await api.get(`/api/Product?${query.toString()}`);

      if (!res) {
        throw new Error("Invalid product response");
      }

      return res;
    },
  });
}
