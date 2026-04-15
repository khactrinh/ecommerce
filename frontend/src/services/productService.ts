import { api } from "@/lib/api";
import { Product, PaginatedResponse, ApiResponse } from "@/lib/types";

export const getProducts = async (page = 1, pageSize = 12) => {
  const res = await api.get<ApiResponse<PaginatedResponse<Product>>>(
    `/api/Product?Page=${page}&PageSize=${pageSize}`
  );
  return res.data;
};

export async function getProductById(id: string) {
  const res = await api.get<ApiResponse<Product>>(`/api/Product/${id}`);
  return res.data;
}
