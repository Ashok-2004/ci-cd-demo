# Phase 1: Project Setup

Phase 1 creates the starting structure for EmployeeHub Demo.

## Backend

The backend is an ASP.NET Core Web API in the `backend` folder.

`Program.cs` is the entry point. It registers controllers, enables OpenAPI in development, maps a simple `/health` endpoint, and prepares the app for future API controllers.

## Backend Tests

The `backend.Tests` folder contains xUnit tests.

The first test is intentionally simple. Its job is to prove that the test project is connected so the future CI pipeline can run backend tests.

## Frontend

The frontend is a React, TypeScript, and Vite app in the `frontend` folder.

The starter screen is intentionally small. It confirms that React, Tailwind, Vite, Axios, and Vitest are installed and organized.

## Why This Structure Matters

This structure keeps each responsibility separate:

- Controllers receive requests.
- Services hold business logic.
- Repositories handle database access.
- Models describe database tables.
- DTOs describe API input and output.
- React pages show screens.
- React services call the backend API.
- Axios sends HTTP requests.

That separation makes the CI/CD presentation easier because every pipeline step has a clear purpose.
