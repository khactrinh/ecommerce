"use client";

import { useEffect, useState } from "react";
import { api } from "@/lib/api";
import { Category } from "@/lib/types";

export function useCategories() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api
      .get<Category[]>("/api/categories/tree")
      .then((res) => {
        setCategories(res.data || res);
      })
      .catch((err) => console.error("Failed to fetch categories", err))
      .finally(() => setLoading(false));
  }, []);

  return { categories, loading };
}
