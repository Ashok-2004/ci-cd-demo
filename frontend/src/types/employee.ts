/*
 * employee.ts defines TypeScript shapes for employee data.
 *
 * Responsibility:
 * - Help React understand what an employee object should look like.
 * - Match the DTOs returned by the ASP.NET Core employee endpoints.
 *
 * Connection to other files:
 * - employeeService.ts returns these types from Axios calls.
 * - DashboardPage.tsx and EmployeeForm.tsx use them to render and edit employees.
 */
export type Employee = {
  id: number
  fullName: string
  email: string
  department: string
  jobTitle: string
  phoneNumber: string | null
  hireDate: string
  createdAt: string
}

export type EmployeeInput = {
  fullName: string
  email: string
  department: string
  jobTitle: string
  phoneNumber: string
  hireDate: string
}

export type DashboardSummary = {
  totalEmployees: number
  recentEmployees: Employee[]
}
