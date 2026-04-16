import { api } from "@/lib/api";
import { Product, PaginatedResponse, ApiResponse } from "@/lib/types";

export const getProducts = async (page = 1, pageSize = 12) => {
  const res = await api.get<PaginatedResponse<Product>>(
    `/api/Product?Page=${page}&PageSize=${pageSize}`
  );
  return res;
};

export async function getProductById(id: string) {
  const res = await api.get<Product>(`/api/Product/${id}`);
  return res;
}
