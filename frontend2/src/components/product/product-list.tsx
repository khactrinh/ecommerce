import ProductCard from "./product-card";
import { Product } from "@/types/product";

export default function ProductList({ items }: { items: Product[] }) {
  return (
    <div className="grid grid-cols-4 gap-4">
      {items.map((p) => (
        <ProductCard key={p.id} product={p} />
      ))}
    </div>
  );
}
