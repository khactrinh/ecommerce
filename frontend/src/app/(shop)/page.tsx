import HeroBanner from "@/components/home/hero-banner";
import CategoryList from "@/components/home/category-list";
import ProductSection from "@/components/home/product-section";
import { getProducts } from "@/services/productService";

export default async function HomePage() {
  // 🚀 fetch server-side (SEO + fast)
  const data = await getProducts(1, 10);

  return (
    <div className="space-y-6">
      <HeroBanner />
      <CategoryList />

      <ProductSection title="Sản phẩm nổi bật" products={data.items} />
    </div>
  );
}
