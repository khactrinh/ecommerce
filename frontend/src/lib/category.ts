import { Category } from "@/lib/types";

export function findCategoryBySlug(
  categories: Category[],
  slug: string,
): Category | null {
  for (const cat of categories) {
    if (cat.slug === slug) return cat;

    if (cat.children?.length) {
      const found = findCategoryBySlug(cat.children, slug);
      if (found) return found;
    }
  }

  return null;
}
