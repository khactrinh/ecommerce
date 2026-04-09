import { fetcher } from "@/lib/api";

export interface Product {
  id: string;
  name: string;
  price: number;
  stock: number;
}

export interface ProductResponse {
  items: Product[];
  page: number;
  pageSize: number;
  total: number;
  totalPages: number;
}

export const getProducts = (page = 1, pageSize = 12) => {
  return fetcher<ProductResponse>(
    `/api/Product?page=${page}&pageSize=${pageSize}`,
  );
};

export async function getProductById(id: string) {
  return fetcher<Product>(`/api/Product/${id}`);
}
