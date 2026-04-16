import { create } from "zustand";
import * as cartApi from "@/services/cartService";
import type { CartItem } from "@/lib/types";

type CartState = {
  items: CartItem[];
  totalItems: number;
  totalPrice: number;
  loading: boolean;

  loadCart: () => Promise<void>;
  addItem: (item: { productId: string; quantity: number }) => Promise<void>;
  removeItem: (productId: string) => Promise<void>;
  updateQuantity: (productId: string, quantity: number) => Promise<void>;
  clearCart: () => void;
};

export const useCart = create<CartState>((set, get) => ({
  items: [],
  totalItems: 0,
  totalPrice: 0,
  loading: false,

  loadCart: async () => {
    const token = typeof window !== "undefined" ? localStorage.getItem("auth_token") : null;
    if (!token) return;

    try {
      set({ loading: true });

      const items = await cartApi.getCart();

      const totalItems = items.reduce((s, i) => s + i.quantity, 0);

      const totalPrice = items.reduce((s, i) => s + i.price * i.quantity, 0);

      set({
        items,
        totalItems,
        totalPrice,
        loading: false,
      });
    } catch (e) {
      console.error(e);
      set({ loading: false });
    }
  },

  addItem: async (item) => {
    await cartApi.addToCart(item);
    await get().loadCart();
  },

  removeItem: async (productId) => {
    await cartApi.removeItem(productId);
    await get().loadCart();
  },

  updateQuantity: async (productId, quantity) => {
    if (quantity <= 0) {
      await cartApi.removeItem(productId);
    } else {
      await cartApi.updateQuantity({
        productId,
        quantity,
      });
    }

    await get().loadCart();
  },

  clearCart: () => {
    set({
      items: [],
      totalItems: 0,
      totalPrice: 0,
    });
  },
}));
