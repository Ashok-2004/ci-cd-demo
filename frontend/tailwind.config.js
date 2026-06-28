/** @type {import('tailwindcss').Config} */
export default {
  /*
   * Tailwind scans these files for class names.
   * This connects React components and pages to generated CSS utilities.
   */
  content: ['./index.html', './src/**/*.{ts,tsx}'],
  theme: {
    extend: {},
  },
  plugins: [],
}
