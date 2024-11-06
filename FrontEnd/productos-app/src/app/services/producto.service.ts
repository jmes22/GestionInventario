import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Producto, ProductosResponse, ErrorResponse } from '../models/producto.model';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {
  private apiUrl = 'https://localhost:7187/api/Producto';
  
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
    withCredentials: true
  };

  constructor(private http: HttpClient) { }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Ha ocurrido un error';
    if (error.error instanceof ErrorEvent) {
      // Error del cliente
      errorMessage = error.error.message;
    } else {
      // Error del servidor
      const serverError = error.error as ErrorResponse;
      if (serverError.message) {
        errorMessage = serverError.message;
      }
    }
    return throwError(() => errorMessage);
  }

  // Obtener todos los productos
  getProductos(): Observable<Producto[]> {
    return this.http.get<ProductosResponse>(this.apiUrl, this.httpOptions)
      .pipe(
        map(response => response.data),
        catchError(this.handleError)
      );
  }

  // Obtener un producto por ID
  getProductoById(id: number): Observable<Producto> {
    return this.http.get<Producto>(`${this.apiUrl}/${id}`, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  searchProductosByNombre(nombre: string): Observable<Producto[]> {
    return this.http.get<ProductosResponse>(`${this.apiUrl}/search?nombre=${encodeURIComponent(nombre)}`, this.httpOptions)
      .pipe(
        map(response => response.data),
        catchError(this.handleError)
      );
  }

  // Agregar nuevo producto
  agregarProducto(producto: Producto): Observable<Producto> {
    return this.http.post<Producto>(this.apiUrl, producto, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // Actualizar producto existente
  actualizarProducto(id: number, producto: Producto): Observable<Producto> {
    // Creamos un nuevo objeto con solo los campos que queremos actualizar
    const productoUpdate = {
      productoId: id,
      nombre: producto.nombre,
      precio: producto.precio,
      cantidad: producto.cantidad
    };

    return this.http.put<Producto>(`${this.apiUrl}/${id}`, productoUpdate, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // Eliminar producto
  eliminarProducto(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }
}