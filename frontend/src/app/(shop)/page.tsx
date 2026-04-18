// "use client";
//
// import HeroBanner from "@/components/home/hero-banner";
// import CategoryList from "@/components/home/category-list";
// import ProductSection from "@/components/home/product-section";
//
// import { useProducts } from "@/hooks/useProducts";
// import { useCategories } from "@/hooks/useCategories";
//
// export default function HomePage() {
//     // 🔥 Products (đã unwrap từ ApiResponse → chỉ còn Paginated<Product>)
//     const {
//         data: productData,
//         isLoading: productLoading,
//         error: productError,
//     } = useProducts(1, 8);
//
//     // 🔥 Categories (đã unwrap → trả về list luôn hoặc paginated tùy mày)
//     const {
//         data: categoryData,
//         isLoading: categoryLoading,
//         error: categoryError,
//     } = useCategories();
//
//     return (
//         <div className="bg-gray-50">
//             {/* ================= HERO ================= */}
//             <section className="max-w-7xl mx-auto px-4 pt-6">
//                 <HeroBanner />
//             </section>
//
//             {/* ================= CATEGORY ================= */}
//             <section className="max-w-7xl mx-auto px-4 mt-10">
//                 <h2 className="text-xl font-bold mb-4">Danh mục nổi bật</h2>
//
//                 {categoryLoading ? (
//                     <div>Loading categories...</div>
//                 ) : categoryError ? (
//                     <div className="text-red-500">
//                         {(categoryError as Error).message}
//                     </div>
//                 ) : (
//                     <CategoryList
//                         categories={
//                             // 🔥 nếu API trả paginated
//                             (categoryData as any)?.items ??
//                             // 🔥 nếu API trả list thẳng
//                             categoryData ??
//                             []
//                         }
//                     />
//                 )}
//             </section>
//
//             {/* ================= FLASH SALE ================= */}
//             <section className="max-w-7xl mx-auto px-4 mt-12">
//                 <div className="flex items-center justify-between mb-4">
//                     <h2 className="text-xl font-bold text-red-500">⚡ Flash Sale</h2>
//                     <span className="text-sm text-gray-500">
//             Limited time deals
//           </span>
//                 </div>
//
//                 {productLoading ? (
//                     <div>Loading products...</div>
//                 ) : productError ? (
//                     <div className="text-red-500">
//                         {(productError as Error).message}
//                     </div>
//                 ) : (
//                     <ProductSection
//                         title=""
//                         products={productData?.items ?? []}
//                     />
//                 )}
//             </section>
//
//             {/* ================= FEATURED ================= */}
//             <section className="max-w-7xl mx-auto px-4 mt-12">
//                 <ProductSection
//                     title="🔥 Sản phẩm nổi bật"
//                     products={productData?.items ?? []}
//                 />
//             </section>
//
//             {/* ================= TRENDING ================= */}
//             <section className="max-w-7xl mx-auto px-4 mt-12 pb-16">
//                 <h2 className="text-xl font-bold mb-4">📈 Trending Now</h2>
//
//                 <ProductSection
//                     title=""
//                     products={productData?.items ?? []}
//                 />
//             </section>
//         </div>
//     );
// }

"use client";

import HeroBanner from "@/components/home/hero-banner";
import CategoryList from "@/components/home/category-list";
import ProductSection from "@/components/home/product-section";

import { useProducts } from "@/features/products/hooks/useProducts";
import { useCategories } from "@/features/categories/hooks/useCategories";

export default function HomePage() {
    const {
        data: productData,
        isLoading: productLoading,
        error: productError,
    } = useProducts(1, 8);

    const {
        data: categoryData,
        isLoading: categoryLoading,
        error: categoryError,
    } = useCategories();

    return (
        <div className="bg-gray-50">
            {/* HERO */}
            <section className="max-w-7xl mx-auto px-4 pt-6">
                <HeroBanner />
            </section>

            {/* CATEGORY */}
            <section className="max-w-7xl mx-auto px-4 mt-10">
                <h2 className="text-xl font-bold mb-4">Danh mục nổi bật</h2>

                {categoryLoading ? (
                    <div>Loading categories...</div>
                ) : categoryError ? (
                    <div className="text-red-500">
                        {(categoryError as Error).message}
                    </div>
                ) : (
                    <CategoryList categories={categoryData ?? []} />
                )}
            </section>

            {/* FLASH SALE */}
            <section className="max-w-7xl mx-auto px-4 mt-12">
                <div className="flex items-center justify-between mb-4">
                    <h2 className="text-xl font-bold text-red-500">⚡ Flash Sale</h2>
                    <span className="text-sm text-gray-500">
            Limited time deals
          </span>
                </div>

                {productLoading ? (
                    <div>Loading products...</div>
                ) : productError ? (
                    <div className="text-red-500">
                        {(productError as Error).message}
                    </div>
                ) : (
                    <ProductSection
                        title=""
                        products={productData?.items ?? []}
                    />
                )}
            </section>

            {/* FEATURED */}
            <section className="max-w-7xl mx-auto px-4 mt-12">
                <ProductSection
                    title="🔥 Sản phẩm nổi bật"
                    products={productData?.items ?? []}
                />
            </section>

            {/* TRENDING */}
            <section className="max-w-7xl mx-auto px-4 mt-12 pb-16">
                <h2 className="text-xl font-bold mb-4">📈 Trending Now</h2>

                <ProductSection
                    title=""
                    products={productData?.items ?? []}
                />
            </section>
        </div>
    );
}