# Request Flow

This is the main learning path for the project.

```mermaid
flowchart TD
    A["React Component"] --> B["Axios"]
    B --> C["EmployeeController"]
    C --> D["EmployeeService"]
    D --> E["EmployeeRepository"]
    E --> F["Entity Framework Core"]
    F --> G["SQL Server"]
    G --> F
    F --> E
    E --> D
    D --> C
    C --> B
    B --> A["React UI"]
```

## Example: Search Employee

1. User types a search term in React
2. `DashboardPage.tsx` calls `employeeService.getEmployees(search)`
3. `employeeService.ts` uses Axios
4. Axios sends `GET /api/employees?search=value`
5. `EmployeesController.cs` receives the request
6. `EmployeeService.cs` applies business flow
7. `EmployeeRepository.cs` queries SQL Server through EF Core
8. SQL Server returns matching rows
9. The API returns JSON
10. React updates the table
