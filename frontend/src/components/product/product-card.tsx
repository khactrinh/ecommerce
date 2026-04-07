"use client";

import Image from "next/image";
import { Product } from "@/types/product";
import Link from "next/link";

export default function ProductCard({ product }: { product: Product }) {
  return (
    <Link href={`/products/${product.id}`}>
      <div className="group border rounded-2xl p-3 bg-white shadow-sm hover:shadow-md transition-all duration-300">
        {/* Image */}
        <div className="relative w-full aspect-square overflow-hidden rounded-xl">
          <Image
            src={product.imageUrl || "/placeholder.png"}
            alt={product.name}
            fill
            className="object-cover group-hover:scale-105 transition-transform duration-300"
          />

          {/* Badge */}
          {product.stock === 0 && (
            <div className="absolute top-2 left-2 bg-red-500 text-white text-xs px-2 py-1 rounded">
              Hết hàng
            </div>
          )}
        </div>

        {/* Info */}
        <div className="mt-3 space-y-1">
          <h3 className="text-sm font-medium line-clamp-2 min-h-[40px]">
            {product.name}
          </h3>

          <p className="text-lg font-semibold text-red-500">
            {product.price.toLocaleString()}₫
          </p>

          <p className="text-xs text-gray-500">Còn {product.stock} sản phẩm</p>
        </div>

        {/* Action */}
        <button className="mt-3 w-full bg-black text-white py-2 rounded-lg text-sm hover:bg-gray-800 transition">
          Thêm vào giỏ
        </button>
      </div>
    </Link>
  );
}
