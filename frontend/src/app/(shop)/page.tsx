import HeroBanner from "@/components/home/hero-banner";
import CategoryList from "@/components/home/category-list";
import ProductSection from "@/components/home/product-section";
import { api } from "@/lib/api";
import { Product, PaginatedResponse, ApiResponse } from "@/lib/types";

export default async function HomePage() {
  let products: Product[] = [];
  
  try {
    const res = await api.get<PaginatedResponse<Product>>("/api/Product?PageSize=10");
    products = res.items;
  } catch (error) {
    console.error("Failed to fetch products for home page", error);
    // In production, you might show a fallback or notify the user
  }

  return (
    <div className="max-w-7xl mx-auto px-6 py-6 space-y-12">
      <HeroBanner />
      
      <div className="space-y-2">
        <h2 className="text-3xl font-extrabold text-gray-900 tracking-tight">Browse Categories</h2>
        <p className="text-gray-500 font-medium">Find exactly what you need from our curated collections.</p>
        <CategoryList />
      </div>

      {products.length > 0 ? (
        <ProductSection title="Featured Products" products={products} />
      ) : (
        <div className="py-20 text-center bg-gray-50 rounded-3xl border-2 border-dashed border-gray-200">
           <p className="text-gray-500 font-medium">Coming soon! We're stocking up on the latest tech.</p>
        </div>
      )}
    </div>
  );
}
