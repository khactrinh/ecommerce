import ProductGallery from "@/components/product/product-gallery";
import ProductInfo from "@/components/product/product-info";
import { getProductById } from "@/services/productService";
import Container from "@/components/ui/container";

export default async function ProductDetailPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;

  const product = await getProductById(id);

  return (
    <div className="max-w-7xl mx-auto p-6">
      <Container>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-10">
          <ProductGallery product={product} />
          <ProductInfo product={product} />
        </div>
      </Container>

      {/* Description */}
      <div className="mt-12 border-t pt-6">
        <h2 className="text-xl font-semibold mb-4">Mô tả sản phẩm</h2>
        <p className="text-gray-600 leading-relaxed">
          {product.description || "Chưa có mô tả"}
        </p>
      </div>
    </div>
  );
}
