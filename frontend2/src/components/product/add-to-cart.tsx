"use client";

import { Product } from "@/types/product";
import { useState } from "react";

export default function AddToCart({ product }: { product: Product }) {
  const [qty, setQty] = useState(1);

  return (
    <div className="space-y-3">
      {/* Quantity */}
      <div className="flex items-center gap-2">
        <button
          onClick={() => setQty(Math.max(1, qty - 1))}
          className="px-3 py-1 border"
        >
          -
        </button>
        <span>{qty}</span>
        <button onClick={() => setQty(qty + 1)} className="px-3 py-1 border">
          +
        </button>
      </div>

      {/* Buttons */}
      <div className="flex gap-3">
        <button className="flex-1 bg-black text-white py-3 rounded-lg">
          Thêm vào giỏ
        </button>

        <button className="flex-1 border py-3 rounded-lg">Mua ngay</button>
      </div>
    </div>
  );
}
