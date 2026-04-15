"use client";

import { useState } from "react";
import { useCart } from "@/store/cart-store";
import { Product } from "@/lib/types";
import Button from "@/components/ui/button";

export default function ProductInfo({ product }: { product: Product }) {
  const [qty, setQty] = useState(1);
  const { addItem, updateQuantity } = useCart();

  const handleAddToCart = () => {
    // Add multiple if quantity > 1
    for (let i = 0; i < qty; i++) {
        addItem(product);
    }
  };

  return (
    <div className="space-y-8">
      <div className="space-y-2">
        <h1 className="text-4xl font-extrabold text-gray-900 tracking-tight">{product.name}</h1>
        <div className="flex items-center gap-4">
            <div className="flex text-yellow-500">
                {[1,2,3,4,5].map(i => (
                    <svg key={i} xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="currentColor"><path d="M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z"/></svg>
                ))}
            </div>
            <span className="text-sm text-gray-500 font-medium">(120 Reviews)</span>
        </div>
      </div>

      <div className="flex items-baseline gap-4">
        <span className="text-4xl font-black text-primary">
          ${product.price.toLocaleString()}
        </span>
        {product.price > 1000 && (
            <span className="text-lg text-gray-400 line-through font-medium">
                ${(product.price * 1.2).toLocaleString()}
            </span>
        )}
      </div>

      <p className="text-gray-600 leading-relaxed text-lg">
        {product.description || "Elevate your daily experience with this premium product. Designed for those who appreciate quality and attention to detail."}
      </p>

      <div className="space-y-4 pt-6 border-t border-gray-100">
        <div className="flex items-center justify-between">
            <span className="text-sm font-bold uppercase tracking-wider text-gray-900">Quantity</span>
            <div className="flex items-center bg-gray-100 rounded-full p-1">
                <button
                    onClick={() => setQty((q) => Math.max(1, q - 1))}
                    className="w-10 h-10 flex items-center justify-center hover:bg-white rounded-full transition shadow-sm disabled:opacity-50"
                    disabled={qty <= 1}
                >
                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M5 12h14"/></svg>
                </button>
                <span className="w-12 text-center font-bold text-lg">{qty}</span>
                <button
                    onClick={() => setQty((q) => q + 1)}
                    className="w-10 h-10 flex items-center justify-center hover:bg-white rounded-full transition shadow-sm"
                >
                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M5 12h14"/><path d="M12 5v14"/></svg>
                </button>
            </div>
        </div>

        <div className="flex gap-4">
            <Button
                onClick={handleAddToCart}
                className="flex-1 h-14 bg-gray-900 text-white rounded-full text-lg font-bold hover:bg-gray-800 transition shadow-lg"
            >
                Add to Cart
            </Button>
            <Button
                variant="outline"
                className="w-14 h-14 rounded-full flex items-center justify-center border-2 border-gray-200 hover:border-primary hover:text-primary transition"
            >
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M19 14c1.49-1.46 3-3.21 3-5.5A5.5 5.5 0 0 0 16.5 3c-1.76 0-3 .5-4.5 2-1.5-1.5-2.74-2-4.5-2A5.5 5.5 0 0 0 2 8.5c0 2.3 1.5 4.05 3 5.5l7 7Z"/></svg>
            </Button>
        </div>
      </div>

      <div className="grid grid-cols-2 gap-4 pt-8">
        <div className="flex items-center gap-3 p-4 bg-gray-50 rounded-2xl">
            <div className="w-10 h-10 bg-white rounded-full flex items-center justify-center shadow-sm text-primary">
                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><rect width="20" height="14" x="2" y="5" rx="2"/><line x1="2" x2="22" y1="10" y2="10"/></svg>
            </div>
            <div>
                <p className="text-xs font-bold text-gray-900">Secure Payment</p>
                <p className="text-[10px] text-gray-500">100% Secure Transaction</p>
            </div>
        </div>
        <div className="flex items-center gap-3 p-4 bg-gray-50 rounded-2xl">
            <div className="w-10 h-10 bg-white rounded-full flex items-center justify-center shadow-sm text-primary">
                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M5 18 3.5 16.5"/><path d="M14 18V6a2 2 0 0 0-2-2H4a2 2 0 0 0-2 2v11a1 1 0 0 0 1 1h2"/><path d="M15 18H9"/><path d="M19 18h2a1 1 0 0 0 1-1v-4.2a2 2 0 0 0-.6-1.4l-3-3A2 2 0 0 0 17 8h-2"/><circle cx="7" cy="18" r="2"/><circle cx="17" cy="18" r="2"/></svg>
            </div>
            <div>
                <p className="text-xs font-bold text-gray-900">Free Shipping</p>
                <p className="text-[10px] text-gray-500">Orders over $500</p>
            </div>
        </div>
      </div>
    </div>
  );
}
