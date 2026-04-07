"use client";

import { useProducts } from "@/hooks/useProducts";
import ProductGrid from "@/components/product/product-grid";
import { useSearchParams } from "next/navigation";

export default function ProductsPage() {
  const searchParams = useSearchParams();
  const page = Number(searchParams.get("page") || 1);

  const { data, isLoading } = useProducts(page, 12);

  if (isLoading) {
    return <div className="p-6">Loading...</div>;
  }

  const totalPages = Math.ceil(data.total / data.pageSize);

  return (
    <div className="p-6 max-w-7xl mx-auto">
      {/* Header */}
      <h1 className="text-2xl font-bold mb-6">Sản phẩm</h1>

      {/* Grid */}
      <ProductGrid products={data.items} />

      {/* Pagination */}
      <div className="mt-8 flex justify-center gap-2">
        {Array.from({ length: totalPages }).map((_, i) => (
          <a
            key={i}
            href={`?page=${i + 1}`}
            className={`px-3 py-1 border rounded ${
              i + 1 === page ? "bg-black text-white" : ""
            }`}
          >
            {i + 1}
          </a>
        ))}
      </div>
    </div>
  );
}
