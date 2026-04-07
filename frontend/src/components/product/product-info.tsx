"use client";

import { useState } from "react";

export default function ProductInfo({ product }: any) {
  const [qty, setQty] = useState(1);

  return (
    <div className="space-y-6">
      {/* Name */}
      <h1 className="text-2xl font-bold">{product.name}</h1>

      {/* Price */}
      <div className="text-3xl font-semibold text-red-500">
        {product.price.toLocaleString()}đ
      </div>

      {/* Rating fake */}
      <div className="text-yellow-500">★★★★★ (120 đánh giá)</div>

      {/* Stock */}
      <div className="text-sm text-gray-500">Còn {product.stock} sản phẩm</div>

      {/* Quantity */}
      <div className="flex items-center gap-3">
        <button
          onClick={() => setQty((q) => Math.max(1, q - 1))}
          className="px-3 py-1 border rounded"
        >
          -
        </button>

        <span>{qty}</span>

        <button
          onClick={() => setQty((q) => q + 1)}
          className="px-3 py-1 border rounded"
        >
          +
        </button>
      </div>

      {/* Actions */}
      <div className="flex gap-4">
        <button className="flex-1 bg-red-500 text-white py-3 rounded-lg hover:bg-red-600">
          Mua ngay
        </button>

        <button className="flex-1 border py-3 rounded-lg hover:bg-gray-100">
          Thêm vào giỏ
        </button>
      </div>

      {/* Shipping */}
      <div className="text-sm text-gray-500">🚚 Giao hàng toàn quốc - COD</div>
    </div>
  );
}
