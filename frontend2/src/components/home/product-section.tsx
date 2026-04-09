import ProductCard from "@/components/product/product-card";
import { Product } from "@/types/product";

export default function ProductSection({
  title,
  products,
}: {
  title: string;
  products: Product[];
}) {
  return (
    <div className="bg-white p-4 rounded-xl">
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-xl font-semibold">{title}</h2>
        <a href="/products" className="text-sm text-blue-500">
          Xem tất cả →
        </a>
      </div>

      <div className="grid grid-cols-2 md:grid-cols-5 gap-4">
        {products.map((p) => (
          <ProductCard key={p.id} product={p} />
        ))}
      </div>
    </div>
  );
}
