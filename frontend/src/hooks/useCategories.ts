// "use client";

// import { useEffect, useState } from "react";
// import { api } from "@/lib/api";
// import { Category } from "@/lib/types";

// export function useCategories() {
//   const [categories, setCategories] = useState<Category[]>([]);
//   const [loading, setLoading] = useState(true);

//   useEffect(() => {
//     api
//       .get<Category[]>("/api/categories/tree")
//       .then((res) => {
//         setCategories(res.data || res);
//       })
//       .catch((err) => console.error("Failed to fetch categories", err))
//       .finally(() => setLoading(false));
//   }, []);

//   return { categories, loading };
// }

import { useQuery } from "@tanstack/react-query";
import { api } from "@/lib/api";
import { Category } from "@/lib/types";

export function useCategories() {
  return useQuery<Category[]>({
    queryKey: ["categories"],

    queryFn: async () => {
      const res = await api.get("/api/categories/tree");

      // 🔥 normalize data (rất quan trọng)
      return res ?? [];
    },

    staleTime: 1000 * 60 * 10, // cache 10 phút
    gcTime: 1000 * 60 * 30, // giữ cache 30 phút
  });
}
