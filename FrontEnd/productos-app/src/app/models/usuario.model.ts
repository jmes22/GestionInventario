export interface Usuario {
    usuarioId: number;
    email: string;
    password: string;
  }
  
  export interface AuthResponse {
    user: Usuario;
    token: string;
  }