/*
 * PostCSS runs CSS plugins during the Vite build.
 * Tailwind generates utility classes and Autoprefixer adds browser prefixes when needed.
 */
export default {
  plugins: {
    tailwindcss: {},
    autoprefixer: {},
  },
}
