# GitHub Actions Guide

GitHub Actions runs automation when repository events happen.

This project uses `.github/workflows/ci-cd.yml`.

## CI Pipeline

Continuous Integration checks that the code still works.

The workflow:

1. Checks out the repository
2. Installs .NET 9
3. Restores backend packages
4. Builds the backend
5. Runs backend tests
6. Installs Node.js
7. Installs frontend packages
8. Runs frontend tests
9. Builds the React app
10. Builds Docker images

## CD Pipeline

Continuous Delivery prepares output for deployment.

This workflow uploads:

- Published backend files
- Built frontend files

It does not deploy to a cloud provider because this is a demo project.

## Why Artifacts Matter

Artifacts are downloadable build outputs from a workflow run.

They prove that the pipeline produced something deployable.
