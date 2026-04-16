"use client";

import Link from "next/link";
import Button from "@/components/ui/button";

export default function CartEmpty() {
  return (
    <div className="text-center py-20 space-y-4">
      <h1 className="text-3xl font-bold">Cart is empty</h1>

      <Link href="/">
        <Button>Go Shopping</Button>
      </Link>
    </div>
  );
}
