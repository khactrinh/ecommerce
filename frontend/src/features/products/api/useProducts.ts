import { useQuery } from "@tanstack/react-query";
import { api } from "@/lib/api";
import { ApiResponse, Paginated } from "@/types/api";
import { Product } from "../types";

export function useProducts(
    page: number,
    pageSize: number,
    categoryId?: string
) {
    return useQuery({
        queryKey: ["products", page, pageSize, categoryId],

        queryFn: async () => {
            const params = new URLSearchParams();

            params.append("page", page.toString());
            params.append("pageSize", pageSize.toString());

            if (categoryId) {
                params.append("categoryId", categoryId);
            }

            const res = await api.get<ApiResponse<Paginated<Product>>>(
                `/api/Product?${params.toString()}`
            );

            if (!res.success) {
                throw new Error(res.message);
            }

            return res.data; // 🔥 luôn trả về data sạch
        },
    });
}