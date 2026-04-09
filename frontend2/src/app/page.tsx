import Header from "@/components/Header";
import Hero from "@/components/Hero";
import ProductCard from "@/components/ProductCard";
//import ProductCard from "@/components/ProductCard";
import products from "@/data/products";

export default function Home() {
  return (
    <div className="bg-gray-100 min-h-screen">
      <Header />

      <div className="max-w-7xl mx-auto px-6 py-6">
        {/* HERO */}
        <Hero />

        {/* CATEGORY */}
        <div className="grid grid-cols-2 md:grid-cols-4 gap-4 mt-6">
          {["Laptop", "Phone", "Tablet", "Accessories"].map((c) => (
            <div
              key={c}
              className="flex items-center gap-3 p-4 bg-white rounded-xl shadow-sm hover:shadow-md cursor-pointer"
            >
              <span>📦</span>
              <span className="font-medium">{c}</span>
            </div>
          ))}
        </div>

        {/* FEATURED */}
        <div className="mt-10 flex justify-between items-center">
          <h2 className="text-xl font-bold">Sản phẩm nổi bật</h2>
          <span className="text-blue-500 cursor-pointer">Xem tất cả →</span>
        </div>

        {/* GRID */}
        <div className="grid grid-cols-2 md:grid-cols-5 gap-5 mt-4">
          {products.map((p, i) => (
            <ProductCard key={i} product={p} />
          ))}
        </div>
      </div>
    </div>
  );
}
