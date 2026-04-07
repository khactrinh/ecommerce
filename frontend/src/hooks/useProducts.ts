"use client";

import { useQuery } from "@tanstack/react-query";
import { getProducts } from "@/services/productService";

export function useProducts(page = 1, pageSize = 12) {
  return useQuery({
    queryKey: ["products", page, pageSize],
    queryFn: () => getProducts(page, pageSize),

    placeholderData: (prev) => prev, // 👈 thay cho keepPreviousData
    staleTime: 1000 * 60,
    retry: false,
  });
}
