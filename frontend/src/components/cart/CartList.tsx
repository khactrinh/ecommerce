"use client";

import CartItemCard from "./CartItemCard";

export default function CartList({ items, removeItem, updateQuantity }: any) {
  return (
    <div className="flex-1 space-y-6">
      {items.map((item: any) => (
        <CartItemCard
          key={item.productId}
          item={{
            id: item.productId,
            name: item.productName,
            price: item.price,
            quantity: item.quantity,
            imageUrl: item.imageUrl || "/placeholder.png",
          }}
          onRemove={removeItem}
          onUpdate={updateQuantity}
        />
      ))}
    </div>
  );
}
