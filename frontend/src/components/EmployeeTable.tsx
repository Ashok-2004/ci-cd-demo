import { Pencil, Trash2 } from 'lucide-react'
import type { Employee } from '../types/employee'

/*
 * EmployeeTable.tsx displays employee records.
 *
 * Responsibility:
 * - Render employees in a scannable table.
 * - Provide edit and delete actions to the page.
 *
 * Connection to other files:
 * - DashboardPage.tsx owns the data and passes handlers.
 * - employeeService.ts refreshes data after changes.
 */
type EmployeeTableProps = {
  employees: Employee[]
  onEdit: (employee: Employee) => void
  onDelete: (employee: Employee) => Promise<void>
}

export function EmployeeTable({ employees, onEdit, onDelete }: EmployeeTableProps) {
  if (employees.length === 0) {
    return (
      <div className="rounded border border-dashed border-slate-300 bg-white p-6 text-sm text-slate-600">
        No employees found.
      </div>
    )
  }

  return (
    <div className="overflow-x-auto rounded border border-slate-200 bg-white shadow-sm">
      <table className="min-w-full divide-y divide-slate-200 text-sm">
        <thead className="bg-slate-50 text-left text-xs font-semibold uppercase text-slate-500">
          <tr>
            <th className="px-4 py-3">Employee</th>
            <th className="px-4 py-3">Department</th>
            <th className="px-4 py-3">Job title</th>
            <th className="px-4 py-3">Hire date</th>
            <th className="px-4 py-3 text-right">Actions</th>
          </tr>
        </thead>
        <tbody className="divide-y divide-slate-100">
          {employees.map((employee) => (
            <tr key={employee.id}>
              <td className="px-4 py-3">
                <p className="font-medium text-slate-950">{employee.fullName}</p>
                <p className="text-slate-500">{employee.email}</p>
              </td>
              <td className="px-4 py-3 text-slate-700">{employee.department}</td>
              <td className="px-4 py-3 text-slate-700">{employee.jobTitle}</td>
              <td className="px-4 py-3 text-slate-700">{employee.hireDate}</td>
              <td className="px-4 py-3">
                <div className="flex justify-end gap-2">
                  <button
                    type="button"
                    className="inline-flex h-9 w-9 items-center justify-center rounded border border-slate-200 text-slate-600 hover:bg-slate-50"
                    onClick={() => onEdit(employee)}
                    aria-label={`Edit ${employee.fullName}`}
                  >
                    <Pencil className="h-4 w-4" aria-hidden="true" />
                  </button>
                  <button
                    type="button"
                    className="inline-flex h-9 w-9 items-center justify-center rounded border border-red-200 text-red-700 hover:bg-red-50"
                    onClick={() => onDelete(employee)}
                    aria-label={`Delete ${employee.fullName}`}
                  >
                    <Trash2 className="h-4 w-4" aria-hidden="true" />
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}
