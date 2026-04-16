"use client";

import { useState } from "react";

type Props = {
  item: {
    id: string;
    name: string;
    price: number;
    quantity: number;
    imageUrl: string;
  };
  onRemove: (id: string) => Promise<any>;
  onUpdate: (id: string, quantity: number) => Promise<any>;
};

export default function CartItemCard({ item, onRemove, onUpdate }: Props) {
  const [loading, setLoading] = useState(false);

  const handleUpdate = async (quantity: number) => {
    try {
      setLoading(true);
      await onUpdate(item.id, quantity);
    } finally {
      setLoading(false);
    }
  };

  const handleRemove = async () => {
    try {
      setLoading(true);
      await onRemove(item.id);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="flex gap-6 p-6 bg-white rounded-3xl border shadow-sm transition hover:shadow-md">
      {/* Image */}
      <div className="w-32 h-32 bg-gray-50 rounded-2xl overflow-hidden">
        <img
          src={item.imageUrl}
          alt={item.name}
          className="w-full h-full object-cover"
        />
      </div>

      {/* Content */}
      <div className="flex-1 flex flex-col justify-between">
        {/* Top */}
        <div className="flex justify-between items-start">
          <div>
            <h3 className="font-bold text-lg text-gray-900">{item.name}</h3>
            <p className="text-sm text-gray-400">Model: 2026 Edition</p>
          </div>

          <button
            disabled={loading}
            onClick={handleRemove}
            className="text-gray-400 hover:text-red-500 transition p-2 disabled:opacity-50"
          >
            ❌
          </button>
        </div>

        {/* Bottom */}
        <div className="flex justify-between items-end">
          {/* Quantity */}
          <div className="flex items-center bg-gray-50 rounded-full p-1 border">
            <button
              disabled={loading}
              onClick={() => handleUpdate(item.quantity - 1)}
              className="w-8 h-8 flex items-center justify-center hover:bg-white rounded-full disabled:opacity-50"
            >
              -
            </button>

            <span className="w-10 text-center font-bold text-sm">
              {item.quantity}
            </span>

            <button
              disabled={loading}
              onClick={() => handleUpdate(item.quantity + 1)}
              className="w-8 h-8 flex items-center justify-center hover:bg-white rounded-full disabled:opacity-50"
            >
              +
            </button>
          </div>

          {/* Price */}
          <span className="font-black text-xl text-gray-900">
            ${(item.price * item.quantity).toLocaleString()}
          </span>
        </div>
      </div>
    </div>
  );
}
