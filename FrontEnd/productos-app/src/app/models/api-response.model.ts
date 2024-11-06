export interface ApiResult<T> {
    isSuccess: boolean;
    data: T;
    error: string | null;
    statusCode: number;
  }