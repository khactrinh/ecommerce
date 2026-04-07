import ProductGallery from "@/components/product/product-gallery";
import ProductInfo from "@/components/product/product-info";
import { getProductById } from "@/services/productService";

export async function generateMetadata({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const product = await getProductById((await params).id);

  return {
    title: product.name,
    description: product.name,
  };
}

export default async function ProductDetailPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;

  console.log("Fetching product with ID:", id);

  const product = await getProductById(id);

  if (!product) {
    return <div className="p-6">Product not found</div>;
  }

  return (
    <div className="max-w-7xl mx-auto p-6 grid md:grid-cols-2 gap-8">
      <ProductGallery product={product} />
      <ProductInfo product={product} />
    </div>
  );
}
