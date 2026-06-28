import { KeyRound, UserPlus } from 'lucide-react'
import { useState, type FormEvent } from 'react'
import type { LoginRequest, RegisterRequest } from '../types/auth'

/*
 * AuthPage.tsx renders login and register forms.
 *
 * Responsibility:
 * - Collect credentials from the user.
 * - Call the auth functions passed from useAuth through App.tsx.
 *
 * Connection to other files:
 * - App.tsx renders this page when the user is not logged in.
 * - authService.ts sends these values to AuthController.cs.
 */
type AuthPageProps = {
  onLogin: (request: LoginRequest) => Promise<void>
  onRegister: (request: RegisterRequest) => Promise<void>
}

type Mode = 'login' | 'register'

export function AuthPage({ onLogin, onRegister }: AuthPageProps) {
  const [mode, setMode] = useState<Mode>('login')
  const [fullName, setFullName] = useState('')
  const [email, setEmail] = useState('admin@employeehub.local')
  const [password, setPassword] = useState('Admin@123')
  const [error, setError] = useState('')
  const [isSubmitting, setIsSubmitting] = useState(false)

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    setError('')
    setIsSubmitting(true)

    try {
      if (mode === 'login') {
        await onLogin({ email, password })
      } else {
        await onRegister({ fullName, email, password })
      }
    } catch {
      setError('Authentication failed. Check the form values and try again.')
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <main className="grid min-h-screen place-items-center px-6 py-10">
      <section className="w-full max-w-md rounded border border-slate-200 bg-white p-6 shadow-sm">
        <p className="text-sm font-semibold uppercase tracking-wide text-sky-700">
          EmployeeHub Demo
        </p>
        <h1 className="mt-2 text-2xl font-bold text-slate-950">
          {mode === 'login' ? 'Login' : 'Register'}
        </h1>

        <div className="mt-5 grid grid-cols-2 rounded border border-slate-200 p-1">
          <button
            type="button"
            className={`rounded px-3 py-2 text-sm font-semibold ${
              mode === 'login' ? 'bg-slate-950 text-white' : 'text-slate-600'
            }`}
            onClick={() => setMode('login')}
          >
            Login
          </button>
          <button
            type="button"
            className={`rounded px-3 py-2 text-sm font-semibold ${
              mode === 'register' ? 'bg-slate-950 text-white' : 'text-slate-600'
            }`}
            onClick={() => setMode('register')}
          >
            Register
          </button>
        </div>

        <form className="mt-5 grid gap-4" onSubmit={handleSubmit}>
          {mode === 'register' ? (
            <label className="grid gap-1 text-sm font-medium text-slate-700">
              Full name
              <input
                className="rounded border border-slate-300 px-3 py-2 text-sm"
                value={fullName}
                onChange={(event) => setFullName(event.target.value)}
                required
              />
            </label>
          ) : null}

          <label className="grid gap-1 text-sm font-medium text-slate-700">
            Email
            <input
              className="rounded border border-slate-300 px-3 py-2 text-sm"
              type="email"
              value={email}
              onChange={(event) => setEmail(event.target.value)}
              required
            />
          </label>

          <label className="grid gap-1 text-sm font-medium text-slate-700">
            Password
            <input
              className="rounded border border-slate-300 px-3 py-2 text-sm"
              type="password"
              value={password}
              onChange={(event) => setPassword(event.target.value)}
              required
            />
          </label>

          {error ? (
            <p className="rounded border border-red-200 bg-red-50 px-3 py-2 text-sm text-red-700">
              {error}
            </p>
          ) : null}

          <button
            type="submit"
            className="inline-flex items-center justify-center gap-2 rounded bg-sky-700 px-4 py-2 text-sm font-semibold text-white hover:bg-sky-800 disabled:cursor-not-allowed disabled:opacity-70"
            disabled={isSubmitting}
          >
            {mode === 'login' ? (
              <KeyRound className="h-4 w-4" aria-hidden="true" />
            ) : (
              <UserPlus className="h-4 w-4" aria-hidden="true" />
            )}
            {isSubmitting ? 'Working' : mode === 'login' ? 'Login' : 'Register'}
          </button>
        </form>
      </section>
    </main>
  )
}
