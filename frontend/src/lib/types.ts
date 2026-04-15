export interface Category {
  id: string;
  name: string;
  slug: string;
  parentId: string | null;
}

export interface Product {
  id: string;
  name: string;
  price: number;
  imageUrl: string;
  stock: number;
  description?: string;
  categoryId?: string;
}

export interface PaginatedResponse<T> {
  items: T[];
  page: number;
  pageSize: number;
  totalItems: number;
  totalPages: number;
  hasNext: boolean;
  hasPrevious: boolean;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
  errors: any;
  traceId: string | null;
}
