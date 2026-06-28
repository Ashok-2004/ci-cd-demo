import axios from 'axios'
import { LogOut, RefreshCw, Search, Users } from 'lucide-react'
import { useEffect, useState, type FormEvent } from 'react'
import { EmployeeForm } from '../components/EmployeeForm'
import { EmployeeTable } from '../components/EmployeeTable'
import { StatusCard } from '../components/StatusCard'
import * as employeeService from '../services/employeeService'
import type { AuthResponse } from '../types/auth'
import type { DashboardSummary, Employee, EmployeeInput } from '../types/employee'

/*
 * DashboardPage.tsx is the main protected React page.
 *
 * Responsibility:
 * - Show dashboard numbers and recent employees.
 * - Search employees and perform add, update, and delete operations.
 *
 * Connection to other files:
 * - App.tsx renders this page after login.
 * - employeeService.ts sends Axios requests to EmployeesController.cs.
 * - EmployeeForm.tsx and EmployeeTable.tsx handle reusable UI pieces.
 */
type DashboardPageProps = {
  auth: AuthResponse
  onLogout: () => void
}

export function DashboardPage({ auth, onLogout }: DashboardPageProps) {
  const [summary, setSummary] = useState<DashboardSummary | null>(null)
  const [employees, setEmployees] = useState<Employee[]>([])
  const [selectedEmployee, setSelectedEmployee] = useState<Employee | null>(null)
  const [search, setSearch] = useState('')
  const [error, setError] = useState('')
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    void loadData()
  }, [])

  async function loadData(nextSearch = search) {
    setError('')
    setIsLoading(true)

    try {
      const [dashboardResponse, employeesResponse] = await Promise.all([
        employeeService.getDashboardSummary(),
        employeeService.getEmployees(nextSearch),
      ])

      setSummary(dashboardResponse)
      setEmployees(employeesResponse)
    } catch {
      setError('Could not load employees. Confirm the ASP.NET Core API and SQL Server are running, then refresh the page.')
    } finally {
      setIsLoading(false)
    }
  }

  async function handleSubmit(employee: EmployeeInput) {
    setError('')

    try {
      if (selectedEmployee) {
        await employeeService.updateEmployee(selectedEmployee.id, employee)
        setSelectedEmployee(null)
      } else {
        await employeeService.createEmployee(employee)
      }

      await loadData()
    } catch (exception) {
      setError(getErrorMessage(exception))
    }
  }

  async function handleDelete(employee: Employee) {
    setError('')

    try {
      await employeeService.deleteEmployee(employee.id)
      await loadData()
    } catch (exception) {
      setError(getErrorMessage(exception))
    }
  }

  async function handleSearch(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    await loadData(search)
  }

  return (
    <main className="mx-auto flex min-h-screen w-full max-w-7xl flex-col gap-6 px-6 py-6">
      <header className="flex flex-col gap-4 border-b border-slate-200 pb-5 md:flex-row md:items-center md:justify-between">
        <div>
          <p className="text-sm font-semibold uppercase tracking-wide text-sky-700">
            EmployeeHub Demo
          </p>
          <h1 className="mt-1 text-2xl font-bold text-slate-950">Employee management</h1>
          <p className="mt-1 text-sm text-slate-600">Signed in as {auth.fullName}</p>
        </div>
        <button
          type="button"
          className="inline-flex items-center gap-2 rounded border border-slate-200 bg-white px-3 py-2 text-sm font-semibold text-slate-700 hover:bg-slate-50"
          onClick={onLogout}
        >
          <LogOut className="h-4 w-4" aria-hidden="true" />
          Logout
        </button>
      </header>

      <section className="grid gap-4 md:grid-cols-3" aria-label="Dashboard summary">
        <StatusCard
          title="Total employees"
          value={summary ? String(summary.totalEmployees) : '0'}
          Icon={Users}
        />
        <StatusCard
          title="Recent employees"
          value={summary ? String(summary.recentEmployees.length) : '0'}
          Icon={RefreshCw}
        />
        <StatusCard title="API security" value="JWT protected" Icon={LogOut} />
      </section>

      {error ? (
        <p className="rounded border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">
          {error}
        </p>
      ) : null}

      <section className="grid gap-6 lg:grid-cols-[380px_1fr]">
        <EmployeeForm
          selectedEmployee={selectedEmployee}
          onSubmit={handleSubmit}
          onCancelEdit={() => setSelectedEmployee(null)}
        />

        <div className="grid gap-4">
          <form
            className="flex flex-col gap-3 rounded border border-slate-200 bg-white p-4 shadow-sm sm:flex-row"
            onSubmit={handleSearch}
          >
            <label className="sr-only" htmlFor="employee-search">
              Search employees
            </label>
            <input
              id="employee-search"
              className="min-w-0 flex-1 rounded border border-slate-300 px-3 py-2 text-sm"
              placeholder="Search name, email, department, or job title"
              value={search}
              onChange={(event) => setSearch(event.target.value)}
            />
            <button
              type="submit"
              className="inline-flex items-center justify-center gap-2 rounded bg-slate-950 px-4 py-2 text-sm font-semibold text-white hover:bg-slate-800"
            >
              <Search className="h-4 w-4" aria-hidden="true" />
              Search
            </button>
          </form>

          {isLoading ? (
            <div className="rounded border border-slate-200 bg-white p-6 text-sm text-slate-600">
              Loading employees...
            </div>
          ) : (
            <EmployeeTable
              employees={employees}
              onEdit={setSelectedEmployee}
              onDelete={handleDelete}
            />
          )}
        </div>
      </section>
    </main>
  )
}

function getErrorMessage(exception: unknown) {
  if (axios.isAxiosError<{ message?: string }>(exception)) {
    if (exception.response?.status === 401) {
      return 'Your login token is not valid anymore. Click Logout, then login again.'
    }

    return exception.response?.data?.message ?? 'The API request failed. Check the backend logs for details.'
  }

  return 'Something went wrong while saving employee data.'
}
