import { useQuery } from "@tanstack/react-query";
import { getCategoryDetail } from "../api/getCategoryDetail";
import type { CategoryDetail } from "../types/category-detail";

export function useCategoryDetail(id: string) {
    return useQuery<CategoryDetail>({
        queryKey: ["category", id],
        queryFn: () => getCategoryDetail(id),
        enabled: !!id,
    });
}