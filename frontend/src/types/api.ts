export type ApiError = {
  message: string;
  errors?: Record<string, string[]>;
};

export type ApiResponse<T> = {
  success: boolean;
  message: string;
  data: T;
  errors?: any;
  traceId?: string;
};

// export type Paginated<T> = {
//   items: T[];
//   page: number;
//   pageSize: number;
//   totalItems: number;
//   totalPages: number;
//   hasNext: boolean;
//   hasPrevious: boolean;
// };