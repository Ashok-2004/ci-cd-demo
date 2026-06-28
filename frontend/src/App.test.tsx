import { render, screen } from '@testing-library/react'
import { describe, expect, it } from 'vitest'
import App from './App'

/*
 * App.test.tsx is the first frontend test for the demo.
 *
 * Responsibility:
 * - Prove that React, TypeScript, Vitest, and Testing Library are connected.
 * - Give the CI pipeline a frontend test command to run.
 *
 * Connection to other files:
 * - It renders App.tsx, which renders DashboardPage.tsx and StatusCard.tsx.
 */
describe('App', () => {
  it('renders the EmployeeHub demo heading', () => {
    render(<App />)

    expect(screen.getByText('EmployeeHub Demo')).toBeInTheDocument()
  })
})
