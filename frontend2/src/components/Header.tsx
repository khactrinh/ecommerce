export default function Header() {
  return (
    <div className="sticky top-0 z-50 bg-white shadow-sm">
      <div className="flex items-center justify-between px-6 py-3 max-w-7xl mx-auto">
        {/* Logo */}
        <h1 className="text-xl font-bold">MyShop</h1>

        {/* Search */}
        <input
          type="text"
          placeholder="Tìm sản phẩm..."
          className="flex-1 mx-6 px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-black"
        />

        {/* Actions */}
        <div className="flex items-center gap-4">
          {/* Cart */}
          <div className="relative cursor-pointer">
            🛒
            <span className="absolute -top-2 -right-2 bg-red-500 text-white text-xs px-1 rounded-full">
              2
            </span>
          </div>

          {/* User */}
          <div>👤</div>
        </div>
      </div>
    </div>
  );
}
