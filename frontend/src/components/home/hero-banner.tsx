"use client";

import Button from "@/components/ui/button";

export default function HeroBanner() {
  return (
    <div className="relative h-[500px] w-full bg-gray-900 rounded-3xl overflow-hidden shadow-2xl">
      {/* Background Graphic */}
      <div className="absolute inset-0 bg-gradient-to-r from-gray-900 via-gray-900/40 to-transparent z-10" />
      <img
        src="https://images.unsplash.com/photo-1519389950473-acc71958480c?auto=format&fit=crop&q=80&w=2000"
        alt="Hero Background"
        className="absolute inset-0 w-full h-full object-cover"
      />

      {/* Content */}
      <div className="relative z-20 h-full flex flex-col justify-center px-10 md:px-20 max-w-2xl gap-6">
        <div className="space-y-2">
          <span className="text-primary font-bold tracking-widest text-sm uppercase">
            Limited Edition
          </span>
          <h1 className="text-5xl md:text-7xl font-black text-white leading-tight">
            New Tech <br /> Evolution.
          </h1>
        </div>
        <p className="text-gray-300 text-lg">
          Experience the next generation of performance and design. Limited
          stock available now.
        </p>
        <div className="flex gap-4">
          <Button className="px-8 py-4 h-14 hover:text-primary  text-gray-900 hover:bg-gray-200">
            Shop Collection
          </Button>
          <Button
            variant="outline"
            className="px-8 py-4 h-14 text-white border-white hover:bg-white/10"
          >
            Learn More
          </Button>
        </div>
      </div>
    </div>
  );
}
