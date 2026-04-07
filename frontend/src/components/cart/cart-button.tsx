"use client";

import { useState } from "react";

export default function CartButton() {
  const [open, setOpen] = useState(false);

  return (
    <div className="relative">
      <button onClick={() => setOpen(!open)}>🛒</button>

      {open && (
        <div className="absolute right-0 top-10 w-80 bg-white border shadow-lg rounded-lg p-4">
          <p className="font-semibold mb-2">Giỏ hàng</p>
          <p className="text-sm text-gray-500">Chưa có sản phẩm</p>
        </div>
      )}
    </div>
  );
}
