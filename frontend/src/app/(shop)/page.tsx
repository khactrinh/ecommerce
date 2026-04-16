"use client";

import HeroBanner from "@/components/home/hero-banner";
import CategoryList from "@/components/home/category-list";
import ProductSection from "@/components/home/product-section";
import { useProducts } from "@/hooks/useProducts";

export default function HomePage() {
  // Featured products
  const { data } = useProducts(1, 8);

  return (
    <div className="bg-gray-50">
      {/* ================= HERO ================= */}
      <section className="max-w-7xl mx-auto px-4 pt-6">
        <HeroBanner />
      </section>

      {/* ================= CATEGORY ================= */}
      <section className="max-w-7xl mx-auto px-4 mt-10">
        <div className="mb-4">
          <h2 className="text-xl font-bold">Danh mục nổi bật</h2>
          <p className="text-sm text-gray-500">
            Khám phá sản phẩm theo từng danh mục
          </p>
        </div>

        <CategoryList />
      </section>

      {/* ================= FLASH SALE ================= */}
      <section className="max-w-7xl mx-auto px-4 mt-12">
        <div className="flex items-center justify-between mb-4">
          <h2 className="text-xl font-bold text-red-500">⚡ Flash Sale</h2>

          <span className="text-sm text-gray-500">Limited time deals</span>
        </div>

        <ProductSection title="" products={data?.items ?? []} />
      </section>

      {/* ================= FEATURED ================= */}
      <section className="max-w-7xl mx-auto px-4 mt-12">
        <ProductSection
          title="🔥 Sản phẩm nổi bật"
          products={data?.items ?? []}
        />
      </section>

      {/* ================= TRENDING ================= */}
      <section className="max-w-7xl mx-auto px-4 mt-12 pb-16">
        <h2 className="text-xl font-bold mb-4">📈 Trending Now</h2>

        <ProductSection title="" products={data?.items ?? []} />
      </section>
    </div>
  );
}
