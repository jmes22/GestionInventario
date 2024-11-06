export interface Producto {
  productoId?: number;
  nombre: string;
  precio: number;
  cantidad?: number;
}

export interface ProductosResponse {
  data: Producto[];
  totalRecords: number;
}

export interface ErrorResponse {
  statusCode: number;
  message: string;
  exceptionType: string;
  stackTrace: string | null;
  timestamp: string;
}