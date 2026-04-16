import { api } from "@/lib/api";
import type { CartItem } from "@/lib/types";

export type AddToCartRequest = {
  productId: string;
  quantity: number;
};

export async function getCart() {
  return api.get<CartItem[]>("/api/cart");
}

export async function addToCart(body: AddToCartRequest) {
  return api.post<string>("/api/cart/add", body);
}

export async function updateQuantity(body: AddToCartRequest) {
  return api.post<string>("/api/cart/update", body);
}

export async function removeItem(productId: string) {
  return api.post<string>("/api/cart/remove", { productId });
}
