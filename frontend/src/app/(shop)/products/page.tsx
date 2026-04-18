"use client";

import { useProducts } from "@/features/products/hooks/useProducts";
import ProductGrid from "@/components/product/product-grid";
import { useSearchParams } from "next/navigation";

export default function ProductsPage() {
    const searchParams = useSearchParams();

    const page = Number(searchParams.get("page") || 1);
    const categoryId = searchParams.get("categoryId") || undefined;

    const { data, isLoading } = useProducts(page, 12, categoryId);

    if (isLoading) {
        return <div className="p-6">Loading...</div>;
    }

    if (!data) {
        return <div className="p-6 text-red-500">No data returned from API</div>;
    }

    // ✅ FIX: dùng đúng field backend
    const totalPages = data.totalPages ?? 1;
    const currentPage = page;

    return (
        <div className="p-6 max-w-7xl mx-auto">
            <h1 className="text-2xl font-bold mb-6">Sản phẩm</h1>

            <ProductGrid products={data.items ?? []} />

            <div className="mt-8 flex justify-center gap-2">
                {Array.from({ length: totalPages }).map((_, i) => (
                    <a
                        key={i}
                        href={`?page=${i + 1}${
                            categoryId ? `&categoryId=${categoryId}` : ""
                        }`}
                        className={`px-3 py-1 border rounded ${
                            i + 1 === currentPage ? "bg-black text-white" : ""
                        }`}
                    >
                        {i + 1}
                    </a>
                ))}
            </div>
        </div>
    );
}