import { defineConfig } from 'vitest/config'
import react from '@vitejs/plugin-react'

/*
 * vite.config.ts configures the React development server and production build.
 * It connects React, TypeScript, Vitest, and the browser-based frontend workflow.
 */
export default defineConfig({
  plugins: [react()],
  test: {
    environment: 'jsdom',
    setupFiles: './src/test/setup.ts',
  },
})
