import { api } from "@/lib/api";

export async function checkout() {
  return api.post<{
    orderId: string;
    paymentUrl: string;
  }>("/api/orders/checkout", {});
}
