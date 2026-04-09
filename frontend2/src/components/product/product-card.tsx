"use client";

import Image from "next/image";
import Link from "next/link";
import { Card, Badge, Button } from "@/components/ui";

export default function ProductCard({ product }: any) {
  return (
    <Card>
      <Link href={`/products/${product.id}`}>
        <div className="cursor-pointer">
          <div className="relative w-full h-48">
            <Image
              src={product.imageUrl}
              alt={product.name}
              fill
              sizes="(max-width: 768px) 100vw, 25vw"
              className="object-cover rounded-lg"
            />
          </div>

          <h3 className="mt-2 font-medium line-clamp-2">{product.name}</h3>
        </div>
      </Link>

      <div className="text-primary font-semibold mt-1">
        {product.price.toLocaleString()}đ
      </div>

      <Badge>Hot</Badge>

      <Button className="w-full mt-3">Thêm vào giỏ</Button>
    </Card>
  );
}
