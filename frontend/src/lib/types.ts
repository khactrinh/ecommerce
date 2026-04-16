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

// ✅ ADD THIS
export interface CartItem {
  productId: string;
  productName: string;
  price: number;
  quantity: number;
  imageUrl?: string;
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

// backend bạn đang dùng ApiResponse<T>
export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
  errors: any;
  traceId: string | null;
}
