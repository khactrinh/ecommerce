"use client";

import { useRouter } from "next/navigation";

export default function CategoryList({ categories = [] }) {
  const router = useRouter();

  return (
    <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-6 gap-6">
      {categories.map((cat, idx) => (
        <div
          key={cat.id}
          onClick={() => router.push(`/categories/${cat.slug}`)}
          className="group cursor-pointer relative"
        >
          {/* Background Glow */}
          <div className={`absolute -inset-1 rounded-[32px] opacity-0 group-hover:opacity-100 transition duration-500 blur-xl ${idx % 2 === 0 ? 'bg-blue-400/30' : 'bg-purple-400/30'}`} />

          <div className="relative bg-white/80 backdrop-blur-xl rounded-[32px] p-8 shadow-sm border border-white hover:border-blue-100/50 hover:shadow-2xl hover:-translate-y-2 transition-all duration-300">
            {/* Icon Container */}
            <div className={`w-16 h-16 rounded-2xl flex items-center justify-center mb-6 group-hover:scale-110 transition-transform duration-500 shadow-lg ${idx % 2 === 0 ? 'bg-gradient-to-br from-blue-500 to-blue-600' : 'bg-gradient-to-br from-slate-800 to-slate-900'}`}>
              <span className="text-2xl text-white font-black">
                {cat.name?.charAt(0)}
              </span>
            </div>

            {/* Content */}
            <div className="space-y-1">
              <h3 className="text-sm font-black text-gray-900 group-hover:text-blue-600 transition tracking-tight">
                {cat.name}
              </h3>
              <p className="text-[10px] font-bold text-gray-400 uppercase tracking-widest">
                {cat.children?.length || 0} Subcategories
              </p>
            </div>

            {/* Arrow Indicator */}
            <div className="absolute bottom-6 right-6 opacity-0 group-hover:opacity-100 transition-all transform translate-x-2 group-hover:translate-x-0">
               <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="3" strokeLinecap="round" strokeLinejoin="round" className="text-blue-500"><path d="M5 12h14"/><path d="m12 5 7 7-7 7"/></svg>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
}
