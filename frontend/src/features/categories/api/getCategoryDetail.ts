// import { api } from "@/lib/api";
// import type { CategoryDetail } from "@/types/category-detail";
//
// export async function getCategoryDetail(id: string): Promise<CategoryDetail> {
//     if (!id) throw new Error("Category id is required");
//
//     const res = await api.get<CategoryDetail>(`/api/categories/${id}`);
//     return res;
// }

import { api } from "@/lib/api";
import type { CategoryDetail } from "../types/category-detail";

export async function getCategoryDetail(id: string): Promise<CategoryDetail> {
    const res = await api.get<CategoryDetail>(`/api/categories/${id}`);
    return res;
}