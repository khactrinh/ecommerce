import { useQuery } from "@tanstack/react-query";
import { api } from "@/lib/api";
import { ApiResponse } from "@/types/api";
import { CategoryDetail } from "../types";

export function useCategoryDetail(id: string) {
    return useQuery({
        queryKey: ["category", id],
        enabled: !!id,

        queryFn: async () => {
            const res = await api.get<ApiResponse<CategoryDetail>>(
                `/api/categories/${id}`
            );

            if (!res.success) {
                throw new Error(res.message);
            }

            return res.data;
        },
    });
}