import { useEffect, useState } from 'react'
import { setAuthToken } from '../api/apiClient'
import * as authService from '../services/authService'
import type { AuthResponse, LoginRequest, RegisterRequest } from '../types/auth'

/*
 * useAuth.ts is a custom React hook for authentication state.
 *
 * Responsibility:
 * - Store the logged-in user and JWT token.
 * - Save auth data in localStorage so refresh keeps the session.
 * - Tell Axios when to include or remove the Authorization header.
 *
 * Connection to other files:
 * - App.tsx uses this hook to decide whether to show AuthPage or DashboardPage.
 * - authService.ts performs the actual API calls.
 */
const storageKey = 'employeehub.auth'

export function useAuth() {
  const [auth, setAuth] = useState<AuthResponse | null>(() => {
    const storedAuth = localStorage.getItem(storageKey)

    if (!storedAuth) {
      return null
    }

    const parsedAuth = JSON.parse(storedAuth) as AuthResponse

    // Attach the token immediately so the dashboard can load data right after refresh.
    setAuthToken(parsedAuth.token)

    return parsedAuth
  })

  useEffect(() => {
    setAuthToken(auth?.token ?? null)
  }, [auth])

  async function login(request: LoginRequest) {
    const response = await authService.login(request)
    localStorage.setItem(storageKey, JSON.stringify(response))
    setAuthToken(response.token)
    setAuth(response)
  }

  async function register(request: RegisterRequest) {
    const response = await authService.register(request)
    localStorage.setItem(storageKey, JSON.stringify(response))
    setAuthToken(response.token)
    setAuth(response)
  }

  function logout() {
    localStorage.removeItem(storageKey)
    setAuth(null)
    setAuthToken(null)
  }

  return { auth, login, register, logout }
}
