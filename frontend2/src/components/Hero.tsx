export default function Hero() {
  return (
    <div className="relative rounded-2xl overflow-hidden">
      <img
        src="https://picsum.photos/300/300?random=1"
        className="w-full h-[300px] md:h-[380px] object-cover"
      />

      <div className="absolute inset-0 bg-black/50 flex flex-col justify-center px-6 md:px-12">
        <h1 className="text-2xl md:text-4xl font-bold text-white">
          Big Sale up to 50%
        </h1>

        <p className="text-white/80 mt-2">Laptop · Phone · Accessories</p>

        <div className="mt-4 flex gap-3">
          <button className="bg-white text-black px-5 py-2 rounded-lg font-medium">
            Shop now
          </button>
          <button className="border border-white text-white px-5 py-2 rounded-lg">
            View deals
          </button>
        </div>
      </div>
    </div>
  );
}
