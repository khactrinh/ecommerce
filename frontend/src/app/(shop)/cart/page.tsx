"use client";

import { useCart } from "@/store/cart-store";
import Link from "next/link";
import Button from "@/components/ui/button";

export default function CartPage() {
  const { items, removeItem, updateQuantity, totalPrice, totalItems } = useCart();

  if (items.length === 0) {
    return (
      <div className="max-w-7xl mx-auto px-6 py-24 text-center space-y-6">
        <div className="w-24 h-24 bg-gray-100 rounded-full flex items-center justify-center mx-auto text-gray-400">
            <svg xmlns="http://www.w3.org/2000/svg" width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round"><circle cx="8" cy="21" r="1"/><circle cx="19" cy="21" r="1"/><path d="M2.05 2.05h2l2.66 12.42a2 2 0 0 0 2 1.58h9.78a2 2 0 0 0 1.95-1.57l1.65-7.43H5.12"/></svg>
        </div>
        <h1 className="text-4xl font-black text-gray-900">Your cart is empty</h1>
        <p className="text-gray-500 max-w-sm mx-auto">Looks like you haven't added anything to your cart yet. Explore our latest tech and find something you love!</p>
        <Link href="/">
            <Button className="px-10 py-4 h-14 rounded-full text-lg">Go Shopping</Button>
        </Link>
      </div>
    );
  }

  return (
    <div className="max-w-7xl mx-auto px-6 py-12">
      <h1 className="text-4xl font-black text-gray-900 mb-10 tracking-tight">Shopping Cart</h1>
      
      <div className="flex flex-col lg:flex-row gap-12">
        {/* Cart Items */}
        <div className="flex-1 space-y-6">
          {items.map((item) => (
            <div key={item.id} className="flex gap-6 p-6 bg-white rounded-3xl border border-gray-100 shadow-sm hover:shadow-md transition group">
              <div className="w-32 h-32 bg-gray-50 rounded-2xl overflow-hidden flex-shrink-0">
                <img src={item.imageUrl} alt={item.name} className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" />
              </div>
              
              <div className="flex-1 flex flex-col justify-between">
                <div className="flex justify-between items-start">
                  <div>
                    <h3 className="font-bold text-lg text-gray-900">{item.name}</h3>
                    <p className="text-sm text-gray-400 font-medium">Model: 2026 Edition</p>
                  </div>
                  <button 
                    onClick={() => removeItem(item.id)}
                    className="text-gray-400 hover:text-red-500 transition p-2"
                  >
                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M3 6h18"/><path d="M19 6v14c0 1-1 2-2 2H7c-1 0-2-1-2-2V6"/><path d="M8 6V4c0-1 1-2 2-2h4c1 0 2 1 2 2v2"/><line x1="10" x2="10" y1="11" y2="17"/><line x1="14" x2="14" y1="11" y2="17"/></svg>
                  </button>
                </div>
                
                <div className="flex justify-between items-end">
                  <div className="flex items-center bg-gray-50 rounded-full p-1 border border-gray-100">
                    <button 
                        onClick={() => updateQuantity(item.id, item.quantity - 1)}
                        className="w-8 h-8 flex items-center justify-center hover:bg-white rounded-full transition shadow-sm"
                    >
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M5 12h14"/></svg>
                    </button>
                    <span className="w-10 text-center font-bold text-sm">{item.quantity}</span>
                    <button 
                        onClick={() => updateQuantity(item.id, item.quantity + 1)}
                        className="w-8 h-8 flex items-center justify-center hover:bg-white rounded-full transition shadow-sm"
                    >
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M5 12h14"/><path d="M12 5v14"/></svg>
                    </button>
                  </div>
                  <span className="font-black text-xl text-gray-900">${(item.price * item.quantity).toLocaleString()}</span>
                </div>
              </div>
            </div>
          ))}
        </div>

        {/* Order Summary */}
        <div className="w-full lg:w-96">
          <div className="bg-gray-900 text-white rounded-3xl p-8 sticky top-24 shadow-2xl space-y-6">
            <h2 className="text-2xl font-bold">Order Summary</h2>
            
            <div className="space-y-4 border-b border-white/10 pb-6">
                <div className="flex justify-between text-gray-400 font-medium">
                    <span>Subtotal ({totalItems} items)</span>
                    <span>${totalPrice.toLocaleString()}</span>
                </div>
                <div className="flex justify-between text-gray-400 font-medium">
                    <span>Shipping</span>
                    <span>Free</span>
                </div>
                <div className="flex justify-between text-gray-400 font-medium">
                    <span>Estimated Tax</span>
                    <span>$0.00</span>
                </div>
            </div>
            
            <div className="flex justify-between items-center py-2">
                <span className="text-lg font-bold">Total</span>
                <span className="text-3xl font-black text-primary">${totalPrice.toLocaleString()}</span>
            </div>

            <Button className="w-full py-5 h-16 rounded-full text-lg font-black bg-white text-gray-900 hover:bg-gray-200 transition">
                Checkout Now
            </Button>
            
            <p className="text-[10px] text-gray-500 text-center font-medium leading-relaxed">
                Prices and availability are not guaranteed until the end of the transaction.
                Secure payment powered by E-SHOP.
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}
