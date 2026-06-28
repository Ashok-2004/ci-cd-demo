import type { LucideIcon } from 'lucide-react'

/*
 * StatusCard.tsx is a small reusable React component.
 *
 * Responsibility:
 * - Display one dashboard metric.
 * - Keep repeated card markup out of page files.
 *
 * Connection to other files:
 * - DashboardPage.tsx uses this component for total employees and API status.
 */
type StatusCardProps = {
  title: string
  value: string
  Icon: LucideIcon
}

export function StatusCard({ title, value, Icon }: StatusCardProps) {
  return (
    <article className="rounded border border-slate-200 bg-white p-5 shadow-sm">
      <div className="flex items-center justify-between gap-3">
        <p className="text-sm font-medium text-slate-500">{title}</p>
        <Icon className="h-5 w-5 text-sky-700" aria-hidden="true" />
      </div>
      <p className="mt-2 text-2xl font-semibold text-slate-950">{value}</p>
    </article>
  )
}
