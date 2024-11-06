import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProductoService } from '../services/producto.service';
import { Producto } from '../models/producto.model';

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrl: './productos.component.css'
})
export class ProductosComponent implements OnInit {
  private fb = inject(FormBuilder);
  
  dataSource: MatTableDataSource<Producto>;
  columnas = ['nombre', 'precio', 'cantidad', 'acciones'];
  mostrarFormulario = false;
  modoEdicion = false;
  productoForm: FormGroup;
  productoIdEnEdicion?: number;
  searchTerm: string = '';

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private productoService: ProductoService,
    private snackBar: MatSnackBar
  ) {
    this.dataSource = new MatTableDataSource<Producto>([]);
    this.productoForm = this.fb.group({
      nombre: ['', Validators.required],
      precio: [0, [Validators.required, Validators.min(0)]],
      cantidad: [0]
    });
  }

  ngOnInit(): void {
    this.cargarProductos();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  cargarProductos(): void {
    this.productoService.getProductos().subscribe({
      next: (productos) => {
        this.dataSource.data = productos;
      },
      error: (error) => this.mostrarMensaje(error)
    });
  }

  buscarProducto(): void {
    const nombre = this.searchTerm.trim();
    
    if (!nombre) {
      this.cargarProductos();
      return;
    }

    this.productoService.searchProductosByNombre(nombre).subscribe({
      next: (productos) => {
        this.dataSource.data = productos;
      },
      error: (error) => {
        this.mostrarMensaje(error);
      }
    });
  }
  
  editarProducto(producto: Producto): void {
    this.modoEdicion = true;
    this.productoIdEnEdicion = producto.productoId;
    this.productoForm.patchValue({
      nombre: producto.nombre,
      precio: producto.precio,
      cantidad: producto.cantidad || 0
    });
    this.mostrarFormulario = true;
  }

  guardarProducto(): void {
    if (this.productoForm.valid) {
      const producto = this.productoForm.value;
      
      if (this.modoEdicion && this.productoIdEnEdicion) {
        producto.productoId = this.productoIdEnEdicion;
        this.productoService.actualizarProducto(this.productoIdEnEdicion, producto).subscribe({
          next: () => {
            this.mostrarMensaje('Producto actualizado exitosamente');
            this.resetearFormulario();
            this.cargarProductos();
          },
          error: (error) => this.mostrarMensaje(error)
        });
      } else {
        this.productoService.agregarProducto(producto).subscribe({
          next: () => {
            this.mostrarMensaje('Producto agregado exitosamente');
            this.resetearFormulario();
            this.cargarProductos();
          },
          error: (error) => this.mostrarMensaje(error)
        });
      }
    }
  }

  confirmarEliminar(producto: Producto): void {
    if (confirm(`¿Está seguro de eliminar el producto ${producto.nombre}?`)) {
      this.eliminarProducto(producto.productoId!);
    }
  }

  eliminarProducto(id: number): void {
    this.productoService.eliminarProducto(id).subscribe({
      next: () => {
        this.mostrarMensaje('Producto eliminado exitosamente');
        this.cargarProductos();
      },
      error: (error) => this.mostrarMensaje(error)
    });
  }

  cancelarEdicion(): void {
    this.resetearFormulario();
  }

  resetearFormulario(): void {
    this.mostrarFormulario = false;
    this.modoEdicion = false;
    this.productoIdEnEdicion = undefined;
    this.productoForm.reset({
      nombre: '',
      precio: 0,
      cantidad: 0
    });
  }

  mostrarMensaje(mensaje: string): void {
    this.snackBar.open(mensaje, 'Cerrar', {
      duration: 5000,
      horizontalPosition: 'right',
      verticalPosition: 'top',
      panelClass: mensaje.toLowerCase().includes('error') ? ['error-snackbar'] : ['success-snackbar']
    });
  }
}