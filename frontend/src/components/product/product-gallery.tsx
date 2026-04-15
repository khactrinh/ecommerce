"use client";

import { Product } from "@/lib/types";

export default function ProductGallery({ product }: { product: Product }) {
  return (
    <div className="space-y-4">
      <div className="aspect-square rounded-3xl overflow-hidden bg-gray-50 border border-gray-100 shadow-inner group">
        <img
          src={product.imageUrl}
          alt={product.name}
          className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-700"
        />
      </div>
      
      {/* Thumbnails placeholder */}
      <div className="grid grid-cols-4 gap-4">
        {[1, 2, 3, 4].map((i) => (
          <div 
            key={i} 
            className={`aspect-square rounded-xl overflow-hidden bg-gray-50 border-2 transition cursor-pointer hover:border-primary ${i === 1 ? "border-primary" : "border-transparent"}`}
          >
            <img
              src={product.imageUrl}
              alt={`View ${i}`}
              className="w-full h-full object-cover opacity-60 hover:opacity-100 transition"
            />
          </div>
        ))}
      </div>
    </div>
  );
}
