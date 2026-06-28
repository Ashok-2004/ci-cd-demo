import axios from 'axios'

/*
 * apiClient.ts creates the shared Axios client for HTTP requests.
 *
 * Responsibility:
 * - Store the backend API base URL in one place.
 * - Attach the JWT token to protected requests.
 *
 * Connection to other files:
 * - authService.ts and employeeService.ts import apiClient.
 * - ASP.NET Core controllers receive the HTTP requests sent from here.
 */
export const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5217',
})

export function setAuthToken(token: string | null) {
  if (token) {
    apiClient.defaults.headers.common.Authorization = `Bearer ${token}`
    return
  }

  delete apiClient.defaults.headers.common.Authorization
}
