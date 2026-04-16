"use client";

import Link from "next/link";
import { useAuth } from "@/store/auth-store";
import { useCart } from "@/store/cart-store";

import { useCategories } from "@/hooks/useCategories";
import CategoryMenu from "./category-menu";

export default function Header() {
  const { user, logout } = useAuth();
  const { totalItems } = useCart();
  const { categories } = useCategories();

  return (
    <header className="bg-white/80 backdrop-blur-md border-b sticky top-0 z-50">
      <div className="max-w-7xl mx-auto flex items-center justify-between gap-6 p-4">
        {/* Logo */}
        <Link
          href="/"
          className="text-2xl font-black tracking-tight text-primary"
        >
          E-SHOP
        </Link>

        {/* Categories Desktop */}
        {/* Categories Desktop */}
        <nav className="hidden md:flex items-center gap-6">
          <CategoryMenu categories={categories.slice(0, 5)} />
        </nav>

        {/* Search */}
        <div className="flex-1 hidden sm:block max-w-md">
          <div className="relative group">
            <input
              placeholder="Search products..."
              className="w-full bg-gray-100 border-none rounded-full px-5 py-2.5 text-sm focus:ring-2 focus:ring-primary/20 transition group-hover:bg-gray-200/80"
            />
            <div className="absolute right-4 top-1/2 -translate-y-1/2 opacity-40">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="18"
                height="18"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                strokeWidth="2"
                strokeLinecap="round"
                strokeLinejoin="round"
              >
                <circle cx="11" cy="11" r="8" />
                <path d="m21 21-4.3-4.3" />
              </svg>
            </div>
          </div>
        </div>

        {/* Actions */}
        <div className="flex items-center gap-2">
          <Link
            href="/cart"
            className="relative p-2 hover:bg-gray-100 rounded-full transition"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="22"
              height="22"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              strokeWidth="2"
              strokeLinecap="round"
              strokeLinejoin="round"
            >
              <circle cx="8" cy="21" r="1" />
              <circle cx="19" cy="21" r="1" />
              <path d="M2.05 2.05h2l2.66 12.42a2 2 0 0 0 2 1.58h9.78a2 2 0 0 0 1.95-1.57l1.65-7.43H5.12" />
            </svg>
            {totalItems > 0 && (
              <span className="absolute -top-1 -right-1 bg-primary text-white text-[10px] font-bold w-5 h-5 rounded-full flex items-center justify-center">
                {totalItems}
              </span>
            )}
          </Link>

          {user ? (
            <div className="flex items-center gap-4 ml-2">
              <div className="hidden lg:block text-right">
                <p className="text-xs font-semibold text-gray-900">
                  {user.email}
                </p>
                <button
                  onClick={logout}
                  className="text-[10px] text-red-500 hover:underline"
                >
                  Logout
                </button>
              </div>
              <div className="w-9 h-9 bg-primary/10 rounded-full flex items-center justify-center text-primary border border-primary/20 font-bold">
                {user.email[0].toUpperCase()}
              </div>
            </div>
          ) : (
            <Link
              href="/login"
              className="ml-2 bg-gray-900 text-white px-5 py-2 rounded-full text-sm font-medium hover:bg-gray-800 transition"
            >
              Đăng nhập
            </Link>
          )}
        </div>
      </div>
    </header>
  );
}
