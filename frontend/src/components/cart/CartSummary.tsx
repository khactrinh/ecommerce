"use client";

import Button from "@/components/ui/button";

export default function CartSummary({
  totalItems,
  totalPrice,
  onCheckout,
}: any) {
  return (
    <div className="w-full lg:w-96">
      <div className="bg-gray-900 text-white rounded-3xl p-8 space-y-6">
        <h2 className="text-2xl font-bold">Order Summary</h2>

        <div className="flex justify-between">
          <span>Items ({totalItems})</span>
          <span>${totalPrice.toLocaleString()}</span>
        </div>

        <div className="flex justify-between text-xl font-bold">
          <span>Total</span>
          <span>${totalPrice.toLocaleString()}</span>
        </div>

        <Button onClick={onCheckout} className="w-full">
          Checkout Now
        </Button>
      </div>
    </div>
  );
}
