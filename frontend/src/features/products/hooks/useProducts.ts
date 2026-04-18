import { useQuery } from "@tanstack/react-query";
import { getProducts } from "../api/getProducts";
import type { Paginated } from "@/shared/types/pagination";
import type { Product } from "../types/product";

export function useProducts(
    page: number,
    pageSize: number,
    categoryId?: string
) {
    return useQuery<Paginated<Product>>({
        queryKey: ["products", page, pageSize, categoryId],
        queryFn: () => getProducts(page, pageSize, categoryId),
    });
}