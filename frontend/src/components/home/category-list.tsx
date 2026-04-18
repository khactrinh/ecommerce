"use client";

import { useRouter } from "next/navigation";
import type { Category } from "@/features/categories/types/category";

export default function CategoryList({
                                         categories = [],
                                     }: {
    categories: Category[];
}) {
    const router = useRouter();

    return (
        <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-6 gap-6">
            {categories.map((cat, idx) => (
                <div
                    key={cat.id}
                    onClick={() => router.push(`/categories/${cat.slug}`)}
                    className="group cursor-pointer relative"
                >
                    <div
                        className={`absolute -inset-1 rounded-[32px] opacity-0 group-hover:opacity-100 blur-xl ${
                            idx % 2 === 0 ? "bg-blue-400/30" : "bg-purple-400/30"
                        }`}
                    />

                    <div className="relative bg-white rounded-[32px] p-8 shadow-sm hover:shadow-xl transition">

                        <div className="w-16 h-16 rounded-2xl flex items-center justify-center mb-6 bg-gray-900 text-white text-2xl font-black">
                            {cat.name.charAt(0)}
                        </div>

                        <h3 className="text-sm font-bold">{cat.name}</h3>

                        <p className="text-xs text-gray-400">
                            {cat.children.length} Subcategories
                        </p>
                    </div>
                </div>
            ))}
        </div>
    );
}