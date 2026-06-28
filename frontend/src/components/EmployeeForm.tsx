import { Save, X } from 'lucide-react'
import { useEffect, useState, type FormEvent } from 'react'
import type { Employee, EmployeeInput } from '../types/employee'

/*
 * EmployeeForm.tsx renders the add/edit employee form.
 *
 * Responsibility:
 * - Collect employee fields from the user.
 * - Reuse the same form for creating and updating employees.
 *
 * Connection to other files:
 * - DashboardPage.tsx passes the selected employee and submit handler.
 * - employeeService.ts sends the submitted data to ASP.NET Core.
 */
type EmployeeFormProps = {
  selectedEmployee: Employee | null
  onSubmit: (employee: EmployeeInput) => Promise<void>
  onCancelEdit: () => void
}

const emptyEmployee: EmployeeInput = {
  fullName: '',
  email: '',
  department: '',
  jobTitle: '',
  phoneNumber: '',
  hireDate: new Date().toISOString().slice(0, 10),
}

export function EmployeeForm({ selectedEmployee, onSubmit, onCancelEdit }: EmployeeFormProps) {
  const [form, setForm] = useState<EmployeeInput>(emptyEmployee)
  const [isSaving, setIsSaving] = useState(false)

  useEffect(() => {
    if (!selectedEmployee) {
      setForm(emptyEmployee)
      return
    }

    setForm({
      fullName: selectedEmployee.fullName,
      email: selectedEmployee.email,
      department: selectedEmployee.department,
      jobTitle: selectedEmployee.jobTitle,
      phoneNumber: selectedEmployee.phoneNumber ?? '',
      hireDate: selectedEmployee.hireDate,
    })
  }, [selectedEmployee])

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    setIsSaving(true)

    try {
      await onSubmit(form)
      setForm(emptyEmployee)
    } finally {
      setIsSaving(false)
    }
  }

  function updateField(field: keyof EmployeeInput, value: string) {
    setForm((current) => ({ ...current, [field]: value }))
  }

  return (
    <form className="rounded border border-slate-200 bg-white p-5 shadow-sm" onSubmit={handleSubmit}>
      <div className="flex items-center justify-between gap-3">
        <h2 className="text-lg font-semibold text-slate-950">
          {selectedEmployee ? 'Edit employee' : 'Add employee'}
        </h2>
        {selectedEmployee ? (
          <button
            type="button"
            className="inline-flex h-9 w-9 items-center justify-center rounded border border-slate-200 text-slate-600 hover:bg-slate-50"
            onClick={onCancelEdit}
            aria-label="Cancel edit"
          >
            <X className="h-4 w-4" aria-hidden="true" />
          </button>
        ) : null}
      </div>

      <div className="mt-4 grid gap-4 md:grid-cols-2">
        <label className="grid gap-1 text-sm font-medium text-slate-700">
          Full name
          <input
            className="rounded border border-slate-300 px-3 py-2 text-sm"
            value={form.fullName}
            onChange={(event) => updateField('fullName', event.target.value)}
            required
          />
        </label>

        <label className="grid gap-1 text-sm font-medium text-slate-700">
          Email
          <input
            className="rounded border border-slate-300 px-3 py-2 text-sm"
            type="email"
            value={form.email}
            onChange={(event) => updateField('email', event.target.value)}
            required
          />
        </label>

        <label className="grid gap-1 text-sm font-medium text-slate-700">
          Department
          <input
            className="rounded border border-slate-300 px-3 py-2 text-sm"
            value={form.department}
            onChange={(event) => updateField('department', event.target.value)}
            required
          />
        </label>

        <label className="grid gap-1 text-sm font-medium text-slate-700">
          Job title
          <input
            className="rounded border border-slate-300 px-3 py-2 text-sm"
            value={form.jobTitle}
            onChange={(event) => updateField('jobTitle', event.target.value)}
            required
          />
        </label>

        <label className="grid gap-1 text-sm font-medium text-slate-700">
          Phone
          <input
            className="rounded border border-slate-300 px-3 py-2 text-sm"
            value={form.phoneNumber}
            onChange={(event) => updateField('phoneNumber', event.target.value)}
          />
        </label>

        <label className="grid gap-1 text-sm font-medium text-slate-700">
          Hire date
          <input
            className="rounded border border-slate-300 px-3 py-2 text-sm"
            type="date"
            value={form.hireDate}
            onChange={(event) => updateField('hireDate', event.target.value)}
            required
          />
        </label>
      </div>

      <button
        type="submit"
        className="mt-5 inline-flex items-center gap-2 rounded bg-sky-700 px-4 py-2 text-sm font-semibold text-white hover:bg-sky-800 disabled:cursor-not-allowed disabled:opacity-70"
        disabled={isSaving}
      >
        <Save className="h-4 w-4" aria-hidden="true" />
        {isSaving ? 'Saving' : selectedEmployee ? 'Update' : 'Add'}
      </button>
    </form>
  )
}
