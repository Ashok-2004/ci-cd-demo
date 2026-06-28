/*
 * auth.ts defines TypeScript shapes for authentication data.
 *
 * Responsibility:
 * - Describe what login/register send to ASP.NET Core.
 * - Describe the JWT response returned by the backend.
 *
 * Connection to other files:
 * - authService.ts uses these types for Axios calls.
 * - useAuth.ts stores AuthResponse in localStorage.
 */
export type LoginRequest = {
  email: string
  password: string
}

export type RegisterRequest = {
  fullName: string
  email: string
  password: string
}

export type AuthResponse = {
  token: string
  email: string
  fullName: string
}
