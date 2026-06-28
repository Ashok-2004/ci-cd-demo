import '@testing-library/jest-dom/vitest'

/*
 * setup.ts runs before frontend tests.
 *
 * Responsibility:
 * - Add helpful DOM assertions such as toBeInTheDocument().
 *
 * Connection to other files:
 * - vite.config.ts points Vitest to this setup file.
 * - Component tests can use these assertions without importing them each time.
 */
