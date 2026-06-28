import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'

/*
 * main.tsx is the frontend entry point.
 *
 * Responsibility:
 * - Find the HTML element with id="root" in index.html.
 * - Mount the React application into that element.
 * - Load global styles that every page can use.
 *
 * Connection to other files:
 * - App.tsx controls the top-level UI.
 * - index.css loads Tailwind and shared styling.
 */
createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
