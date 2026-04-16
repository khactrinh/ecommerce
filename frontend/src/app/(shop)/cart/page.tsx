"use client";

import { useCart } from "@/store/cart-store";
import { checkout } from "@/services/orderService";
import { useEffect } from "react";

import CartEmpty from "@/components/cart/CartEmpty";
import CartList from "@/components/cart/CartList";
import CartSummary from "@/components/cart/CartSummary";

export default function CartPage() {
  const {
    items,
    totalItems,
    totalPrice,
    loading,
    removeItem,
    updateQuantity,
    loadCart,
  } = useCart();

  useEffect(() => {
    loadCart();
  }, [loadCart]);

  const handleCheckout = async () => {
    const res = await checkout();
    window.location.href = res.paymentUrl;
  };

  if (loading) return <div className="p-10">Loading...</div>;

  if (items.length === 0) return <CartEmpty />;

  return (
    <div className="max-w-7xl mx-auto px-6 py-12">
      <h1 className="text-4xl font-black mb-10">Shopping Cart</h1>

      <div className="flex flex-col lg:flex-row gap-12">
        <CartList
          items={items}
          removeItem={removeItem}
          updateQuantity={updateQuantity}
        />

        <CartSummary
          totalItems={totalItems}
          totalPrice={totalPrice}
          onCheckout={handleCheckout}
        />
      </div>
    </div>
  );
}
