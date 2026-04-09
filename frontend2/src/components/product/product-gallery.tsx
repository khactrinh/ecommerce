"use client";

import Image from "next/image";

export default function ProductGallery({ product }: any) {
  return (
    <div className="space-y-4">
      <div className="relative w-full aspect-square bg-gray-100 rounded-xl overflow-hidden">
        <Image
          src={product.imageUrl}
          alt={product.name}
          fill
          sizes="(max-width: 768px) 100vw, 50vw"
          className="object-cover hover:scale-105 transition"
        />
      </div>

      {/* thumbnails (future multi images) */}
      <div className="flex gap-2">
        <div className="w-16 h-16 bg-gray-200 rounded-lg" />
        <div className="w-16 h-16 bg-gray-200 rounded-lg" />
      </div>
    </div>
  );
}
