export interface Product {
  id: string;
  name: string;
  price: number;
  stock: number;
}

export interface PagedResult<T> {
  items: T[];
  page: number;
  pageSize: number;
  total: number;
}
