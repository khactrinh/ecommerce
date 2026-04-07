"use client";

import Image from "next/image";
import { useState } from "react";
import { Product } from "@/types/product";

export default function ProductGallery({ product }: { product: Product }) {
  const [image, setImage] = useState(product.imageUrl);

  return (
    <div>
      {/* Main image */}
      <div className="relative w-full aspect-square rounded-xl overflow-hidden">
        <Image
          src={image || "/placeholder.png"}
          alt={product.name}
          fill
          className="object-cover"
        />
      </div>

      {/* Thumbnails (future multi images) */}
      <div className="flex gap-2 mt-3">
        {[product.imageUrl].map((img, i) => (
          <div
            key={i}
            className="w-16 h-16 relative border rounded cursor-pointer"
            onClick={() => setImage(img)}
          >
            <Image src={img} alt="" fill className="object-cover" />
          </div>
        ))}
      </div>
    </div>
  );
}
