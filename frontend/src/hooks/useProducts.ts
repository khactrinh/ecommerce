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

//VERSION 2: FIXED CATEGORY FILTERING
// export function useProducts(
//   page: number,
//   pageSize: number,
//   categoryId?: string,
// ) {
//   const query = new URLSearchParams();

//   query.append("page", page.toString());
//   query.append("pageSize", pageSize.toString());

//   if (categoryId) {
//     query.append("categoryId", categoryId); // ✅ FIX HERE
//   }

//   return useQuery({
//     queryKey: ["products", page, categoryId],
//     queryFn: async () => {
//       const res = await api.get(`/api/Product?${query.toString()}`);

//       if (!res) {
//         throw new Error("Invalid product response");
//       }

//       return res;
//     },
//   });
// }

// export function useProducts(
//   page: number,
//   pageSize: number,
//   categoryId?: string,
// ) {
//   return useQuery({
//     queryKey: ["products", page, pageSize, categoryId],
//
//     queryFn: async () => {
//       const params = new URLSearchParams({
//         Page: String(page),
//         PageSize: String(pageSize),
//       });
//
//       if (categoryId) {
//         params.append("CategoryId", categoryId);
//       }
//
//       console.log("Fetching products with params:", params.toString());
//
//       const res = await api.get(`/api/Product?${params.toString()}`);
//
//       console.log("useProducts response:", res);
//
//       return res;
//     },
//
//     enabled: categoryId !== undefined, // 🔥 FIX QUAN TRỌNG
//   });
// }

export function useProducts(
  page: number,
  pageSize: number,
  categoryId?: string,
) {
  return useQuery({
    queryKey: ["products", page, pageSize, categoryId],

    // chỉ disable nếu mày THỰC SỰ cần categoryId
    enabled: true, // hoặc !!categoryId nếu business yêu cầu

    queryFn: async () => {
      const query = new URLSearchParams();

      query.append("page", page.toString());
      query.append("pageSize", pageSize.toString());

      if (categoryId) {
        query.append("categoryId", categoryId);
      }

      const res = await api.get(`/api/Product?${query.toString()}`);

      //return res.data; // 🔥 luôn normalize
      return res;
    },
  });
}
