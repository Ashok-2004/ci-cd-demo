import { useAuth } from './hooks/useAuth'
import { AuthPage } from './pages/AuthPage'
import { DashboardPage } from './pages/DashboardPage'

/*
 * App.tsx is the top-level React component.
 *
 * Responsibility:
 * - Decide whether the user should see authentication or the dashboard.
 * - Pass login, register, and logout functions to pages.
 *
 * Connection to other files:
 * - useAuth.ts stores JWT state and configures Axios.
 * - AuthPage.tsx handles login/register forms.
 * - DashboardPage.tsx calls protected employee APIs.
 */
function App() {
  const { auth, login, register, logout } = useAuth()

  if (!auth) {
    return <AuthPage onLogin={login} onRegister={register} />
  }

  return <DashboardPage auth={auth} onLogout={logout} />
}

export default App
