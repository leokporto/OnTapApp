
# OnTapApp


## Introduction
OnTapApp is a solution developed to manage and present information about beers and their styles, providing a modern and efficient experience for users and administrators. The project uses a modern architecture based on a modular monolith, integrating backend, frontend, and cloud/containerized infrastructure.


## Technologies Used
- **.NET 9** (C#)
- **ASP.NET Core minimal APIs**
- **Blazor** (Mixed mode)
- **Docker** and **Docker Compose**
- **.Net Aspire** (AppHost, ServiceDefaults)
- **Dapper** for data access
- **PgSql** as the database server


## Project Structure
```
OnTapApp.sln
├── Aspire/
│   ├── OnTapApp.AppHost/           # Orchestration and configuration of Aspire services
│   └── OnTapApp.ServiceDefaults/   # Shared configurations and extensions
├── OnTapApp.API/                   # Main REST API (ASP.NET Core)
│   ├── Endpoints/                  # Domain endpoints (e.g., Beers)
│   └── Infrastructure/             # Repositories, UnitOfWork, contracts
├── OnTapApp.Core/                  # Domain entities (e.g., Beer)
├── OnTapApp.Web/                   # Blazor Server frontend
│   ├── Components/                 # Components and pages (e.g., Beers, BeerStyles)
│   └── Services/                   # API integration services
├── OnTapApp.Web.Client/            # Blazor WebAssembly frontend (optional)
├── Docker/                         # Docker configuration files
└── docker-compose.yml              # Container orchestration
```


## Future Features
- User registration and authentication using Keycloak
- Beer rating and comments system
- Mobile device integration (maybe)


---


> Project under development. Suggestions and contributions are welcome!

