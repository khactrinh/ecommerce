import { Product } from "@/types/product";

export default function ProductCard({ product }: { product: Product }) {
  return (
    <div className="border rounded-xl p-4 hover:shadow-lg transition">
      <h2 className="font-semibold">{product.name}</h2>
      <p className="text-gray-500">{product.price.toLocaleString()}₫</p>
      <p className="text-gray-500">Stock: {product.stock}</p>
    </div>
  );
}
