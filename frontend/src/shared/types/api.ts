export interface ApiResponse<T> {
    success: boolean;
    message: string;
    data: T;
    errors?: any;
    traceId?: string;
}