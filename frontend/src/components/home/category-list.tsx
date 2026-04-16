"use client";

import { useEffect, useState } from "react";
import { api } from "@/lib/api";
import { Category, PaginatedResponse } from "@/lib/types";
import Link from "next/link";

export default function CategoryList() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api.get<PaginatedResponse<Category>>("/api/categories")
      .then(res => {
        if (res && res.items) {
          setCategories(res.items.filter(c => !c.parentId));
        }
      })
      .catch(err => console.error("Failed to fetch categories", err))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return null;

  return (
    <section className="py-12">
      <div className="grid grid-cols-2 md:grid-cols-4 gap-6">
        {categories.map((cat, i) => (
          <Link
            key={cat.id}
            href={`/categories/${cat.slug}`}
            className="group relative h-48 rounded-2xl overflow-hidden bg-gray-100 flex items-center justify-center p-6 text-center hover:shadow-xl transition-all duration-500"
          >
            {/* Background pattern or subtle color */}
            <div className={`absolute inset-0 opacity-10 group-hover:opacity-20 transition-opacity bg-gradient-to-br ${
                i % 2 === 0 ? "from-primary to-blue-500" : "from-purple-500 to-pink-500"
            }`} />
            
            <div className="relative z-10 space-y-2">
                <h3 className="text-xl font-bold text-gray-900 group-hover:scale-110 transition-transform">
                    {cat.name}
                </h3>
                <p className="text-xs font-semibold uppercase tracking-widest text-gray-500">Explore Collection</p>
            </div>
          </Link>
        ))}
      </div>
    </section>
  );
}
