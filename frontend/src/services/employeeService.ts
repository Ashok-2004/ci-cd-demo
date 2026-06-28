import { apiClient } from '../api/apiClient'
import type { DashboardSummary, Employee, EmployeeInput } from '../types/employee'

/*
 * employeeService.ts contains frontend functions for employee API calls.
 *
 * Responsibility:
 * - Keep Axios calls out of React page components.
 * - Provide one clear function per employee endpoint.
 *
 * Connection to other files:
 * - DashboardPage.tsx calls these functions.
 * - EmployeesController.cs receives the matching ASP.NET Core requests.
 */
export async function getDashboardSummary(): Promise<DashboardSummary> {
  const response = await apiClient.get<DashboardSummary>('/api/employees/dashboard')
  return response.data
}

export async function getEmployees(search: string): Promise<Employee[]> {
  const response = await apiClient.get<Employee[]>('/api/employees', {
    params: { search: search || undefined },
  })
  return response.data
}

export async function createEmployee(employee: EmployeeInput): Promise<Employee> {
  const response = await apiClient.post<Employee>('/api/employees', employee)
  return response.data
}

export async function updateEmployee(id: number, employee: EmployeeInput): Promise<Employee> {
  const response = await apiClient.put<Employee>(`/api/employees/${id}`, employee)
  return response.data
}

export async function deleteEmployee(id: number): Promise<void> {
  await apiClient.delete(`/api/employees/${id}`)
}
