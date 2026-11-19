# Apartment Booking

A backend apartment booking system built with ASP.NET Core 8 and a layered architecture.

## Architecture Overview

```mermaid
flowchart LR
    Client[API Client] --> API

    subgraph API[API Layer]
        Controllers
        Middleware[Swagger / JWT / Dev Auth]
    end

    subgraph BLL[Business Logic Layer]
        Services
        Interfaces
        Exceptions
    end

    subgraph DAL[Data Access Layer]
        Repositories
        EFContext[EF Core DbContext]
        Models
    end

    subgraph Contracts[Shared Contracts]
        DTOs
        Enums
        Configs
    end

    Database[(SQL Server)]

    API --> BLL
    BLL --> DAL
    API --> Contracts
    BLL --> Contracts
    DAL --> Contracts
    Repositories --> EFContext
    EFContext --> Database
```

## Projects

| Project     | Responsibility                                                                                                                      |
| ----------- | ----------------------------------------------------------------------------------------------------------------------------------- |
| `API`       | ASP.NET Core entry point, HTTP controllers, dependency injection, Swagger, JWT authentication, and development middleware.          |
| `BLL`       | Application use cases, business rules, validation, authentication logic, token creation, service interfaces, and domain exceptions. |
| `DAL`       | EF Core database context, entity models, repository implementations, and SQL Server persistence.                                    |
| `Contracts` | Shared DTOs, request and response models, pagination types, configuration classes, and enums.                                       |
