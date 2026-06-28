# Backend Concepts

This document explains the ASP.NET Core backend in beginner-friendly language.

## Controllers

Controllers receive HTTP requests.

`AuthController.cs` handles login and register requests.

`EmployeesController.cs` handles employee CRUD requests.

Controllers should stay thin. They receive requests, call services, and return HTTP responses.

## Services

Services contain business logic.

`EmployeeService.cs` checks rules such as unique employee email addresses.

`AuthService.cs` checks passwords and creates authentication responses.

Services make the app easier to test because business rules are not hidden inside controllers.

## Repository Pattern

Repositories contain database access code.

`EmployeeRepository.cs` uses Entity Framework Core to query and save employees.

`UserRepository.cs` uses Entity Framework Core to query and save users.

The service layer calls repository interfaces, not SQL directly.

## Dependency Injection

Dependency Injection means classes receive what they need instead of creating everything themselves.

`Program.cs` registers services such as:

```csharp
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
```

ASP.NET Core then creates those objects when controllers need them.

## Middleware

Middleware is code that runs during the HTTP request pipeline.

In this project, middleware handles:

- CORS
- Authentication
- Authorization
- Swagger
- Controller routing

## Swagger

Swagger provides browser-based API documentation.

Run the API and open:

```text
http://localhost:5217/swagger
```

Swagger is useful for testing endpoints before the React app calls them.

## Entity Framework Core

Entity Framework Core is an object-relational mapper.

It lets C# code work with database tables through classes:

- `Employee` maps to employee rows
- `User` maps to user rows
- `AppDbContext` represents the database session

## JWT Authentication

JWT means JSON Web Token.

The flow is:

1. User logs in
2. Backend validates email and password
3. Backend returns a signed token
4. React stores the token
5. Axios sends the token in the `Authorization` header
6. ASP.NET Core validates the token before protected endpoints run
