import { apiClient } from '../api/apiClient'
import type { AuthResponse, LoginRequest, RegisterRequest } from '../types/auth'

/*
 * authService.ts contains frontend functions for authentication API calls.
 *
 * Responsibility:
 * - Send login and register requests with Axios.
 * - Return the JWT response to React hooks/components.
 *
 * Connection to other files:
 * - AuthPage.tsx calls these functions through useAuth.ts.
 * - AuthController.cs receives these requests in ASP.NET Core.
 */
export async function login(request: LoginRequest): Promise<AuthResponse> {
  const response = await apiClient.post<AuthResponse>('/api/auth/login', request)
  return response.data
}

export async function register(request: RegisterRequest): Promise<AuthResponse> {
  const response = await apiClient.post<AuthResponse>('/api/auth/register', request)
  return response.data
}
