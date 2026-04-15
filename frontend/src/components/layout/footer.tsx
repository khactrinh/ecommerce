export default function Footer() {
  return (
    <footer className="bg-gray-900 text-white mt-12 pt-16 pb-8">
      <div className="max-w-7xl mx-auto px-6 grid grid-cols-1 md:grid-cols-4 gap-12 border-b border-white/10 pb-12">
        <div className="space-y-6">
          <h2 className="text-2xl font-black tracking-tight">E-SHOP</h2>
          <p className="text-gray-400 text-sm leading-relaxed">
            The ultimate destination for tech enthusiasts. Experience the future of shopping with our curated selection of premium gadgets and electronics.
          </p>
        </div>
        
        <div className="space-y-6">
          <h3 className="font-bold text-sm uppercase tracking-widest text-primary">Quick Links</h3>
          <ul className="space-y-4 text-sm text-gray-400">
            <li><a href="/" className="hover:text-white transition">Home</a></li>
            <li><a href="/products" className="hover:text-white transition">All Products</a></li>
            <li><a href="/login" className="hover:text-white transition">My Account</a></li>
            <li><a href="/cart" className="hover:text-white transition">Shopping Cart</a></li>
          </ul>
        </div>

        <div className="space-y-6">
          <h3 className="font-bold text-sm uppercase tracking-widest text-primary">Categories</h3>
          <ul className="space-y-4 text-sm text-gray-400">
            <li><a href="/categories/electronics" className="hover:text-white transition">Electronics</a></li>
            <li><a href="/categories/fashion" className="hover:text-white transition">Fashion</a></li>
            <li><a href="/categories/home-living" className="hover:text-white transition">Home & Living</a></li>
            <li><a href="/categories/accessories" className="hover:text-white transition">Accessories</a></li>
          </ul>
        </div>

        <div className="space-y-6">
          <h3 className="font-bold text-sm uppercase tracking-widest text-primary">Newsletter</h3>
          <p className="text-xs text-gray-400">Subscribe to receive updates, access to exclusive deals, and more.</p>
          <div className="flex bg-white/5 rounded-full p-1 border border-white/10">
            <input 
                placeholder="Email address" 
                className="bg-transparent border-none px-4 flex-1 text-sm focus:outline-none"
            />
            <button className="bg-white text-gray-900 px-4 py-2 rounded-full text-xs font-bold hover:bg-gray-200 transition">
                Join
            </button>
          </div>
        </div>
      </div>
      
      <div className="max-w-7xl mx-auto px-6 pt-8 flex flex-col md:flex-row justify-between items-center gap-4 text-xs font-medium text-gray-500">
        <p>© 2026 E-SHOP Global Ltd. All rights reserved.</p>
        <div className="flex gap-6">
            <a href="#" className="hover:text-white transition">Privacy Policy</a>
            <a href="#" className="hover:text-white transition">Terms of Service</a>
            <a href="#" className="hover:text-white transition">Cookie Policy</a>
        </div>
      </div>
    </footer>
  );
}
