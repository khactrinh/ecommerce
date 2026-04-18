import { api } from "@/lib/api";
import type { Paginated } from "@/shared/types/pagination";
import type { Product } from "../types/product";

export async function getProducts(
    page: number,
    pageSize: number,
    categoryId?: string
): Promise<Paginated<Product>> {
    const query = new URLSearchParams({
        pageNumber: page.toString(),
        pageSize: pageSize.toString(),
    });

    if (categoryId) {
        query.append("categoryId", categoryId);
    }

    return api.get<Paginated<Product>>(`/api/product?${query}`);
}