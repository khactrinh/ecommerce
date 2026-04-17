// "use client";

// import { useEffect, useState } from "react";
// import Link from "next/link";
// import { useParams } from "next/navigation";
// import { api } from "@/lib/api";
// import { Category, Product, PaginatedResponse } from "@/lib/types";
// import ProductCard from "@/components/product/product-card";

// export default function CategoryPage() {
//   const params = useParams();
//   const slug = params.slug as string;

//   const [category, setCategory] = useState<Category | null>(null);
//   const [products, setProducts] = useState<Product[]>([]);
//   const [loading, setLoading] = useState(true);
//   const [error, setError] = useState<string | null>(null);

//   useEffect(() => {
//     async function fetchData() {
//       if (!slug) return;

//       try {
//         setLoading(true);
//         setError(null);

//         // 1. Fetch categories to find the ID for the slug
//         // Note: We handle both wrapped and unwrapped responses for robustness
//         const catRes = await api.get<any>("/api/categories");

//         // Handle potential ApiResponse wrapper or direct PaginatedResponse
//         const categoriesData = (catRes.data ||
//           catRes) as PaginatedResponse<Category>;

//         if (!categoriesData || !categoriesData.items) {
//           throw new Error("Invalid categories response structure");
//         }

//         const found = categoriesData.items.find(
//           (c: Category) => c.slug === slug,
//         );

//         if (found) {
//           setCategory(found);

//           // 2. Fetch products using the found CategoryId
//           // The api utility automatically unwraps the 'data' field
//           const productsData = await api.get<PaginatedResponse<Product>>(
//             `/api/Product?CategoryId=${found.id}&PageSize=20`,
//           );

//           if (productsData && productsData.items) {
//             setProducts(productsData.items);
//           } else {
//             setProducts([]);
//           }
//         } else {
//           setError("Không tìm thấy danh mục này.");
//         }
//       } catch (err: any) {
//         console.error("Failed to fetch category data:", err);
//         setError(
//           "Có lỗi xảy ra khi tải dữ liệu. Vui lòng kiểm tra kết nối hoặc đăng nhập lại.",
//         );
//       } finally {
//         setLoading(false);
//       }
//     }

//     fetchData();
//   }, [slug]);

//   if (loading)
//     return (
//       <div className="max-w-7xl mx-auto p-10 flex flex-col items-center justify-center min-h-[400px]">
//         <div className="w-12 h-12 border-4 border-primary/20 border-t-primary rounded-full animate-spin"></div>
//         <p className="mt-4 text-gray-500 font-medium">Đang tải sản phẩm...</p>
//       </div>
//     );

//   if (error || !category)
//     return (
//       <div className="max-w-7xl mx-auto p-20 text-center">
//         <h1 className="text-3xl font-black text-gray-900">
//           {error || "Không tìm thấy danh mục"}
//         </h1>
//         <p className="text-gray-500 mt-4">
//           Vui lòng quay lại{" "}
//           <Link href="/" className="text-primary hover:underline font-bold">
//             Trang chủ
//           </Link>
//           .
//         </p>
//       </div>
//     );

//   return (
//     <div className="max-w-7xl mx-auto px-6 py-12">
//       <header className="mb-12 space-y-4">
//         <div className="flex items-center gap-2 text-sm font-bold text-primary uppercase tracking-widest">
//           <Link href="/" className="hover:underline">
//             Home
//           </Link>
//           <span>/</span>
//           <span className="text-gray-400">Categories</span>
//         </div>
//         <h1 className="text-5xl font-black text-gray-900 tracking-tight capitalize">
//           {category.name}
//         </h1>
//         <div className="h-1.5 w-24 bg-primary rounded-full" />
//       </header>

//       <div className="flex flex-col md:flex-row gap-10">
//         {/* Sidebar Filters */}
//         <aside className="w-full md:w-64 space-y-8">
//           <div className="space-y-4">
//             <h3 className="font-bold text-gray-900 uppercase text-xs tracking-widest">
//               Sort By
//             </h3>
//             <div className="space-y-2">
//               {["Featured", "Price: Low to High", "Price: High to Low"].map(
//                 (opt) => (
//                   <label
//                     key={opt}
//                     className="flex items-center gap-3 cursor-pointer group"
//                   >
//                     <div className="w-4 h-4 rounded border-2 border-gray-300 group-hover:border-primary transition" />
//                     <span className="text-sm font-medium text-gray-600 group-hover:text-gray-900">
//                       {opt}
//                     </span>
//                   </label>
//                 ),
//               )}
//             </div>
//           </div>
//         </aside>

//         {/* Product Grid */}
//         <main className="flex-1">
//           <div className="flex justify-between items-center mb-6">
//             <p className="text-sm text-gray-500 font-medium">
//               Showing {products.length} results
//             </p>
//           </div>

//           <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-8">
//             {products.map((p) => (
//               <ProductCard key={p.id} product={p} />
//             ))}
//           </div>

//           {products.length === 0 && (
//             <div className="text-center py-20 bg-gray-50 rounded-3xl border-2 border-dashed border-gray-200">
//               <p className="text-gray-500 font-medium text-lg">
//                 No products found in this category.
//               </p>
//             </div>
//           )}
//         </main>
//       </div>
//     </div>
//   );
// }

"use client";

import Link from "next/link";
import { useParams } from "next/navigation";
import ProductCard from "@/components/product/product-card";
import { useCategoryBySlug } from "@/hooks/useCategoryBySlug";
import { useProducts } from "@/hooks/useProducts";

export default function CategoryPage() {
  const params = useParams();
  const slug = params.slug as string;

  const { data: category, isLoading: catLoading } = useCategoryBySlug(slug);

  const { data: productData, isLoading: prodLoading } = useProducts(
    1,
    20,
    category?.id,
  );

  // ✅ FIX 1: loading riêng
  if (catLoading) {
    return <div className="p-10">Loading category...</div>;
  }

  // ✅ FIX 2: not found riêng
  if (!category) {
    return (
      <div className="p-10 text-center text-red-500">Category not found</div>
    );
  }

  const products = productData?.items ?? [];

  // ✅ loading products
  if (prodLoading) {
    return <div className="p-10">Loading products...</div>;
  }

  return (
    <div className="max-w-7xl mx-auto px-6 py-12">
      <header className="mb-12">
        <h1 className="text-4xl font-bold">{category.name}</h1>
      </header>

      {products.length === 0 ? (
        <div>No products</div>
      ) : (
        <div className="grid grid-cols-3 gap-6">
          {products.map((p) => (
            <ProductCard key={p.id} product={p} />
          ))}
        </div>
      )}
    </div>
  );
}
