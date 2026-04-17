// import { useQuery } from "@tanstack/react-query";
// import { api } from "@/lib/api";
// import { Category } from "@/lib/types";

// export function useCategoryBySlug(slug?: string) {
//   return useQuery<Category | null>({
//     queryKey: ["category", slug],
//     enabled: !!slug,

//     queryFn: async () => {
//       const res = await api.get("/api/categories/tree");
//       const categories: Category[] = res?.data ?? [];

//       // flatten tree
//       const flatten = (cats: Category[]): Category[] =>
//         cats.flatMap((c) => [c, ...(c.children ? flatten(c.children) : [])]);

//       const all = flatten(categories);

//       return all.find((c) => c.slug === slug) ?? null;
//     },

//     staleTime: 1000 * 60 * 10,
//   });
// }

import { useQuery } from "@tanstack/react-query";
import { api } from "@/lib/api";
import { Category } from "@/lib/types";

function flatten(categories: Category[]): Category[] {
  let result: Category[] = [];

  for (const c of categories) {
    result.push(c);

    if (c.children?.length) {
      result = result.concat(flatten(c.children));
    }
  }

  return result;
}

export function useCategoryBySlug(slug?: string) {
  return useQuery<Category | null>({
    queryKey: ["category", slug],
    enabled: !!slug,

    queryFn: async () => {
      const res = await api.get("/api/categories/tree");

      const raw = res?.data ?? res;

      const categories: Category[] = Array.isArray(raw)
        ? raw
        : (raw.data ?? []);

      const all = flatten(categories);

      console.log("slug:", slug);
      console.log("all:", all);

      return (
        all.find((c) => c.slug.toLowerCase() === slug?.toLowerCase()) ?? null
      );
    },
  });
}
