import Image from "next/image";

export default function HeroBanner() {
  return (
    <div className="relative h-56 md:h-80 w-full">
      <Image
        src="https://picsum.photos/1200/400"
        alt="banner"
        fill
        priority // 🚀 preload (quan trọng)
        className="object-cover rounded-xl"
      />

      <div className="absolute inset-0 bg-black/30 flex items-center justify-center text-white text-2xl font-bold">
        Big Sale 🔥
      </div>
    </div>
  );
}
