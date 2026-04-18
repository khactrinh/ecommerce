import { api } from "@/lib/api";
import type { Category } from "../types/category";

export async function getCategories(): Promise<Category[]> {
    const res = await api.get<Category[]>("/api/categories/tree");
    return res ?? [];
}