"use client";

import { Product } from "@/types/product";
import AddToCart from "./add-to-cart";

export default function ProductInfo({ product }: { product: Product }) {
  return (
    <div className="space-y-4">
      {/* Name */}
      <h1 className="text-2xl font-bold">{product.name}</h1>

      {/* Price */}
      <p className="text-3xl text-red-500 font-semibold">
        {product.price.toLocaleString()}₫
      </p>

      {/* Stock */}
      <p className="text-sm text-gray-500">Còn {product.stock} sản phẩm</p>

      {/* Description (fake for now) */}
      <div className="text-sm text-gray-700">
        Sản phẩm chất lượng cao, phù hợp nhu cầu sử dụng hàng ngày.
      </div>

      {/* Action */}
      <AddToCart product={product} />
    </div>
  );
}
