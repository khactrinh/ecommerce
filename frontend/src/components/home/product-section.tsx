import ProductCard from "@/components/product/product-card";
import { Product } from "@/lib/types";
import Link from "next/link";

export default function ProductSection({
  title,
  products,
}: {
  title: string;
  products: Product[];
}) {
  return (
    <section className="py-12">
      <div className="flex justify-between items-end mb-10">
        <div className="space-y-1">
            <h2 className="text-3xl font-bold text-gray-900 tracking-tight">{title}</h2>
            <div className="h-1 w-20 bg-primary rounded-full" />
        </div>
        <Link 
            href="/products" 
            className="group flex items-center gap-2 text-sm font-bold text-primary hover:gap-3 transition-all"
        >
          View all 
          <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="3" strokeLinecap="round" strokeLinejoin="round"><path d="m9 18 6-6-6-6"/></svg>
        </Link>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 xl:grid-cols-5 gap-8">
        {products.map((p) => (
          <ProductCard key={p.id} product={p} />
        ))}
      </div>
    </section>
  );
}
