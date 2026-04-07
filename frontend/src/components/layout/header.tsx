"use client";

import Link from "next/link";

export default function Header() {
  return (
    <header className="bg-white border-b sticky top-0 z-50">
      <div className="max-w-7xl mx-auto flex items-center gap-6 p-4">
        {/* Logo */}
        <Link href="/" className="text-xl font-bold">
          MyShop
        </Link>

        {/* Search */}
        <div className="flex-1">
          <input
            placeholder="Tìm sản phẩm..."
            className="w-full border rounded-lg px-4 py-2 focus:outline-none"
          />
        </div>

        {/* Actions */}
        <div className="flex items-center gap-4">
          <button>🛒</button>
          <button>👤</button>
        </div>
      </div>
    </header>
  );
}
