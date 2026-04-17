export type Category = {
    id: string;
    name: string;
    slug: string;
    parentId?: string | null;
};

export type CategoryDetail = Category & {
    parentName?: string;
    hasChildren: boolean;
};