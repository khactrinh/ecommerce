export interface CategoryDetail {
    id: string;
    name: string;
    slug: string;
    parentId?: string | null;
    children?: CategoryDetail[];
}