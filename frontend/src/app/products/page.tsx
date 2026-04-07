import { fetcher } from "@/lib/api";
import ProductCard from "@/components/product/product-card";
import { PagedResult, Product } from "@/types/product";

async function getProducts(page: number) {
  return fetcher<PagedResult<Product>>(
    `/api/products?page=${page}&pageSize=12`,
  );
}

export default async function ProductsPage({
  searchParams,
}: {
  searchParams: { page?: string };
}) {
  const page = Number(searchParams.page || 1);

  const data = await getProducts(page);

  const totalPages = Math.ceil(data.total / data.pageSize);

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Products</h1>

      <div className="grid grid-cols-4 gap-4">
        {data.items.map((p) => (
          <ProductCard key={p.id} product={p} />
        ))}
      </div>

      {/* Pagination */}
      <div className="mt-6 flex gap-2">
        {Array.from({ length: totalPages }).map((_, i) => (
          <a
            key={i}
            href={`?page=${i + 1}`}
            className="px-3 py-1 border rounded"
          >
            {i + 1}
          </a>
        ))}
      </div>
    </div>
  );
}
