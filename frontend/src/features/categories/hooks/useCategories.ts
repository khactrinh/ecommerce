import { useQuery } from "@tanstack/react-query";
import { getCategories } from "../api/getCategories";
import type { Category } from "../types/category";

export function useCategories() {
    return useQuery<Category[]>({
        queryKey: ["categories"],
        queryFn: getCategories,
    });
}