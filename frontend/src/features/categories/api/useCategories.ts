import { useQuery } from "@tanstack/react-query";
import { api } from "@/lib/api";
import { ApiResponse, Paginated } from "@/types/api";
import { Category } from "../types";

export function useCategories(page: number, pageSize: number) {
    return useQuery({
        queryKey: ["categories", page, pageSize],

        queryFn: async () => {
            const params = new URLSearchParams();

            params.append("page", page.toString());
            params.append("pageSize", pageSize.toString());

            const res = await api.get<ApiResponse<Paginated<Category>>>(
                `/api/categories?${params.toString()}`
            );

            if (!res.success) {
                throw new Error(res.message);
            }

            return res.data;
        },
    });
}