# Docker Guide

Docker lets the project run the same way on different machines.

## Backend Dockerfile

The backend Dockerfile uses two stages:

1. SDK image builds and publishes the API
2. Runtime image runs the published API

This keeps the final image smaller than shipping the full SDK.

## Frontend Dockerfile

The frontend Dockerfile also uses two stages:

1. Node image installs packages and builds React
2. Nginx image serves the static build files

## Docker Compose

`docker-compose.yml` starts:

- SQL Server
- ASP.NET Core API
- React frontend

Run:

```powershell
docker compose up --build
```

Open:

```text
http://localhost:5174
```

## Container Communication

Inside Docker Compose, the backend connects to SQL Server with:

```text
Server=sqlserver,1433
```

That works because Compose creates a network where services can call each other by service name.
