"use client";

import Link from "next/link";
import { Product } from "@/lib/types";
import { useCart } from "@/store/cart-store";

interface ProductCardProps {
  product: Product;
}

export default function ProductCard({ product }: ProductCardProps) {
  const { addItem } = useCart();

  return (
    <div className="group relative bg-white rounded-2xl overflow-hidden shadow-sm hover:shadow-xl transition-all duration-500 border border-gray-100 flex flex-col h-full">
      {/* Image Container */}
      <div className="aspect-square overflow-hidden bg-gray-50 relative">
        <Link href={`/products/${product.id}`}>
          <img
            src={product.imageUrl}
            alt={product.name}
            className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-700"
          />
        </Link>
        <button
          onClick={() => {
            console.log("PRODUCT:", product);

            addItem({
              productId: product.id,
              quantity: 1,
            });
          }}
          className="absolute bottom-4 right-4 bg-white/90 backdrop-blur-sm p-3 rounded-full shadow-lg opacity-0 translate-y-4 group-hover:opacity-100 group-hover:translate-y-0 transition-all duration-300 hover:bg-primary hover:text-white"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="20"
            height="20"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            strokeWidth="2"
            strokeLinecap="round"
            strokeLinejoin="round"
          >
            <path d="M5 12h14" />
            <path d="M12 5v14" />
          </svg>
        </button>
      </div>

      {/* Info */}
      <div className="p-5 flex flex-col flex-1">
        <div className="flex justify-between items-start mb-2">
          <Link href={`/products/${product.id}`} className="flex-1">
            <h3 className="font-semibold text-gray-900 group-hover:text-primary transition line-clamp-2">
              {product.name}
            </h3>
          </Link>
        </div>

        <div className="mt-auto pt-4 flex items-center justify-between">
          <span className="text-xl font-bold text-gray-900">
            ${product.price.toLocaleString()}
          </span>
          <div className="text-[10px] uppercase tracking-wider font-bold text-gray-400">
            {product.stock > 0 ? `${product.stock} in stock` : "Out of stock"}
          </div>
        </div>
      </div>
    </div>
  );
}
