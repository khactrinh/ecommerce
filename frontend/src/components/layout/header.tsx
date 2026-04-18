// "use client";
//
// import Link from "next/link";
// import { useAuth } from "@/store/auth-store";
// import { useCart } from "@/store/cart-store";
//
// import { useCategories } from "@/features/categories/api/useCategories";
// import CategoryMenu from "./category-menu";
//
// export default function Header() {
//   const { user, logout } = useAuth();
//   const { totalItems } = useCart();
//
//   const { data: categories = [], isLoading } = useCategories();
//
//   return (
//       <header className="bg-white/80 backdrop-blur-md border-b sticky top-0 z-50">
//         <div className="max-w-7xl mx-auto flex items-center justify-between gap-6 p-4">
//           {/* Logo */}
//           <Link
//               href="/"
//               className="text-2xl font-black tracking-tight text-primary"
//           >
//             E-SHOP
//           </Link>
//
//           {/* Categories Desktop */}
//           <nav className="hidden md:flex items-center gap-6">
//             {isLoading ? (
//                 <div className="text-sm text-gray-400">Loading...</div>
//             ) : (
//                 <CategoryMenu categories={categories.slice(0, 5)} />
//             )}
//           </nav>
//
//           {/* Search */}
//           <div className="flex-1 hidden sm:block max-w-md">
//             <div className="relative group">
//               <input
//                   placeholder="Search products..."
//                   className="w-full bg-gray-100 border-none rounded-full px-5 py-2.5 text-sm focus:ring-2 focus:ring-primary/20 transition group-hover:bg-gray-200/80"
//               />
//             </div>
//           </div>
//
//           {/* Actions */}
//           <div className="flex items-center gap-2">
//             {/* Cart */}
//             <Link
//                 href="/cart"
//                 className="relative p-2 hover:bg-gray-100 rounded-full transition"
//             >
//               🛒
//               {totalItems > 0 && (
//                   <span className="absolute -top-1 -right-1 bg-primary text-white text-[10px] font-bold w-5 h-5 rounded-full flex items-center justify-center">
//                 {totalItems}
//               </span>
//               )}
//             </Link>
//
//             {/* User */}
//             {user ? (
//                 <div className="flex items-center gap-4 ml-2">
//                   <div className="hidden lg:block text-right">
//                     <p className="text-xs font-semibold text-gray-900">
//                       {user.email}
//                     </p>
//                     <button
//                         onClick={logout}
//                         className="text-[10px] text-red-500 hover:underline"
//                     >
//                       Logout
//                     </button>
//                   </div>
//
//                   <div className="w-9 h-9 bg-primary/10 rounded-full flex items-center justify-center text-primary border border-primary/20 font-bold">
//                     {user.email[0].toUpperCase()}
//                   </div>
//                 </div>
//             ) : (
//                 <Link
//                     href="/login"
//                     className="ml-2 bg-gray-900 text-white px-5 py-2 rounded-full text-sm font-medium hover:bg-gray-800 transition"
//                 >
//                   Đăng nhập
//                 </Link>
//             )}
//           </div>
//         </div>
//       </header>
//   );
// }
//
"use client";

import Link from "next/link";
import { useAuth } from "@/store/auth-store";
import { useCart } from "@/store/cart-store";

import { useCategories } from "@/features/categories/hooks/useCategories";
import CategoryMenu from "./category-menu";

export default function Header() {
  const { user, logout } = useAuth();
  const { totalItems } = useCart();

  const { data: categories = [], isLoading } = useCategories();

  return (
      <header className="bg-white/80 backdrop-blur-md border-b sticky top-0 z-50">
        <div className="max-w-7xl mx-auto flex items-center justify-between gap-6 p-4">

          {/* Logo */}
          <Link href="/" className="text-2xl font-black tracking-tight text-primary">
            E-SHOP
          </Link>

          {/* Categories */}
          <nav className="hidden md:flex items-center gap-6">
            {isLoading ? (
                <div className="text-sm text-gray-400">Loading...</div>
            ) : (
                <CategoryMenu categories={categories.slice(0, 5)} />
            )}
          </nav>

          {/* Search */}
          <div className="flex-1 hidden sm:block max-w-md">
            <input
                placeholder="Search products..."
                className="w-full bg-gray-100 rounded-full px-5 py-2.5 text-sm"
            />
          </div>

          {/* Actions */}
          <div className="flex items-center gap-2">
            <Link href="/cart" className="relative p-2">
              🛒
              {totalItems > 0 && (
                  <span className="absolute -top-1 -right-1 bg-primary text-white text-[10px] w-5 h-5 rounded-full flex items-center justify-center">
                {totalItems}
              </span>
              )}
            </Link>

            {user ? (
                <div className="flex items-center gap-4 ml-2">
                  <div className="hidden lg:block text-right">
                    <p className="text-xs font-semibold">{user.email}</p>
                    <button onClick={logout} className="text-[10px] text-red-500">
                      Logout
                    </button>
                  </div>

                  <div className="w-9 h-9 bg-primary/10 rounded-full flex items-center justify-center">
                    {user.email[0].toUpperCase()}
                  </div>
                </div>
            ) : (
                <Link href="/login" className="ml-2 bg-gray-900 text-white px-5 py-2 rounded-full text-sm">
                  Đăng nhập
                </Link>
            )}
          </div>
        </div>
      </header>
  );
}