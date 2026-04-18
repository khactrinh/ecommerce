import { useQuery, type UseQueryOptions } from "@tanstack/react-query";
import { api } from "./api";

type ApiQueryOptions<T> = Omit<
    UseQueryOptions<T, Error>,
    "queryKey" | "queryFn"
>;

export function useApiQuery<T>(
    queryKey: any[],
    endpoint: string,
    options?: ApiQueryOptions<T>
) {
    return useQuery<T, Error>({
        queryKey,

        queryFn: async (): Promise<T> => {
            const res = await api.get<T>(endpoint);

            if (res === undefined || res === null) {
                throw new Error("API returned empty response");
            }

            return res;
        },

        ...options,
    });
}