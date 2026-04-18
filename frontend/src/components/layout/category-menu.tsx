import { useRouter } from "next/navigation";
import type { Category } from "@/features/categories/types/category";

export default function CategoryMenu({
                                         categories,
                                     }: {
    categories: Category[];
}) {
    const router = useRouter();

    return (
        <>
            {categories.map((cat) => (
                <div key={cat.id} className="relative group inline-block">

                    {/* Parent */}
                    <button
                        onClick={() =>
                            router.push(`/products?categoryId=${cat.id}`)
                        }
                        className="text-sm font-medium text-gray-600 hover:text-primary"
                    >
                        {cat.name}
                    </button>

                    {/* Children */}
                    {cat.children.length > 0 && (
                        <div className="absolute left-0 top-full pt-2 hidden group-hover:block z-50">
                            <div className="bg-white shadow-lg p-2 rounded min-w-[200px]">
                                {cat.children.map((child) => (
                                    <button
                                        key={child.id}
                                        onClick={() =>
                                            router.push(`/products?categoryId=${child.id}`)
                                        }
                                        className="block w-full text-left px-3 py-2 text-sm hover:bg-gray-100 rounded"
                                    >
                                        {child.name}
                                    </button>
                                ))}
                            </div>
                        </div>
                    )}
                </div>
            ))}
        </>
    );
}