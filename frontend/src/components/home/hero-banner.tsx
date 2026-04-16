"use client";

import Button from "@/components/ui/button";
import { useRouter } from "next/navigation";

export default function HeroBanner() {
  const router = useRouter();

  return (
    <div className="relative w-full h-[600px] rounded-[40px] overflow-hidden shadow-[0_20px_50px_rgba(0,0,0,0.1)] group">
      {/* Background Image - THE GENERATED ONE */}
      <img
        src="/hero_tech_premium_1776320547998.png"
        alt="Premium Tech Experience"
        className="absolute inset-0 w-full h-full object-cover scale-100 group-hover:scale-105 transition duration-[2s] ease-out"
      />

      {/* Enhanced Gradient Overlays for High Contrast */}
      <div className="absolute inset-0 bg-gradient-to-r from-black/95 via-black/40 to-transparent z-[1]" />
      <div className="absolute inset-0 bg-gradient-to-t from-black/40 to-transparent z-[1]" />

      {/* Content */}
      <div className="relative z-10 h-full flex items-center px-8 md:px-20">
        <div className="max-w-2xl space-y-8">
          {/* Badge */}
          <div className="inline-flex items-center gap-2 px-5 py-2 rounded-full bg-white/10 backdrop-blur-2xl border border-white/20 shadow-xl">
            <span className="flex h-2 w-2">
              <span className="animate-ping absolute inline-flex h-2 w-2 rounded-full bg-blue-400 opacity-75"></span>
              <span className="relative inline-flex rounded-full h-2 w-2 bg-blue-500"></span>
            </span>
            <span className="text-[10px] md:text-xs text-white font-bold uppercase tracking-[0.2em]">
              New Arrival • 2026 Collection
            </span>
          </div>

          {/* Title */}
          <h1 className="text-5xl md:text-8xl font-black text-white leading-[0.9] tracking-tighter drop-shadow-[0_10px_30px_rgba(0,0,0,0.5)]">
            Elevate Your <br />
            <span className="text-blue-400">Digital Life</span>
          </h1>

          {/* Description */}
          <p className="text-white/90 text-lg md:text-xl font-medium leading-relaxed max-w-lg drop-shadow-md">
            Meticulously crafted hardware for the modern professional. Experience power and elegance in every detail.
          </p>

          {/* Buttons */}
          <div className="flex flex-wrap gap-5">
            <button
              onClick={() => router.push("/products")}
              className="px-10 py-4 bg-white text-black font-bold rounded-2xl hover:bg-blue-500 hover:text-white transition-all transform hover:scale-105 active:scale-95 shadow-2xl"
            >
              Shop Collection
            </button>

            <button
              onClick={() => router.push("/categories")}
              className="px-10 py-4 bg-white/10 backdrop-blur-xl text-white font-bold rounded-2xl border border-white/20 hover:bg-white/20 transition-all shadow-xl"
            >
              Explore Tech
            </button>
          </div>

          {/* Stats/Trust */}
          <div className="flex gap-10 pt-4 opacity-70">
            <div className="text-white">
              <p className="text-2xl font-black">24k+</p>
              <p className="text-[10px] uppercase tracking-widest font-bold">Happy Clients</p>
            </div>
            <div className="text-white">
              <p className="text-2xl font-black">4.9/5</p>
              <p className="text-[10px] uppercase tracking-widest font-bold">Average Rating</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
