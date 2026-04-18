// export interface Product {
//   id: string;
//   name: string;
//   price: number;
//   stock: number;
//   imageUrl: string;
// }
//
// export interface PagedResult<T> {
//   items: T[];
//   page: number;
//   pageSize: number;
//   total: number;
// }

export type Product = {
  id: string;
  name: string;
  price: number;
  imageUrl: string;
  stock: number;
};