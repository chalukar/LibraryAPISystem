## Library API System

A sample modular .NET 9 solution demonstrating a small library domain with REST APIs, gRPC services, CQRS + MediatR, EF Core, and a full automated test suite (unit, integration, functional, and system tests).

---

### Features

- **REST API** (`Library.Api`)
  - Manage books: create, update, delete, list, related books, most borrowed.
  - Borrowers analytics: top borrowers, reading pace.
  - Lending operations: borrow and return books.

- **gRPC Service** (`Library.gRPC`)
  - Exposes library operations with strongly-typed gRPC contracts (`library.proto`).

- **Application Layer** (`Library.Application`)
  - CQRS with MediatR (commands/queries + handlers).
  - DTOs and AutoMapper mapping profiles.

- **Domain Layer** (`Library.Domain`)
  - Rich domain entities: `Book`, `Borrower`, `LendingRecord`.
  - Repository interfaces (ports).

- **Infrastructure Layer** (`Library.Infrastructure`)
  - EF Core 9 `LibraryDbContext`.
  - SQL Server persistence + migrations.
  - Repository implementations.
  - Seed data for demo usage.

- **BuildingBlocks**
  - Common abstractions: `Result`, CQRS interfaces (`ICommand`, `IQuery`, etc.).

- **Tests** (`Library.Tests`)
  - **Unit tests**: domain and application handlers.
  - **Integration tests**: repositories with real SQL Server (test DB).
  - **Functional tests**: HTTP-level tests via `WebApplicationFactory`.
  - **System tests**: end-to-end workflows (e.g., borrow/return flow).

---

### Tech Stack

- **Runtime**: .NET 9
- **API**: ASP.NET Core Web API
- **gRPC**: `Grpc.AspNetCore`, `Grpc.Tools`
- **Data**: Entity Framework Core 9 (SQL Server, InMemory for tests)
- **CQRS / Messaging**: MediatR 13
- **Mapping**: AutoMapper
- **Testing**: xUnit, FluentAssertions, Microsoft.AspNetCore.Mvc.Testing
- **DB Reset in tests**: Respawn

---

### Solution Structure
```
src/
  BuildingBlocks/
    BuildingBlocks/
      Common/Result.cs
      CQRS/...
  Services/
    Library/
      Library.Api/          # REST API (Program, Controllers)
      Library.Application/  # Commands, Queries, Handlers, DTOs, Mapping
      Library.Domain/       # Entities, Repository interfaces
      Library.Infrastructure/ # EF Core DbContext, Migrations, Repositories, SeedData
      Library.gRPC/         # gRPC host + Protos
      Library.Tests/        # Unit, Integration, Functional, System tests---
```
### Getting Started

#### 1. Prerequisites

- .NET 9 SDK
- SQL Server (local or container)
- (Optional) `dotnet-ef` CLI for migrations

#### 2. Database Setup

Update the connection string in `Library.Api/appsettings.json`:
```
"ConnectionStrings": {
  "LibraryDb": "Server=YOUR_SERVER;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
Apply migrations 
```
dotnet ef database update --project src/Services/Library/Library.Infrastructure/Library.Infrastructure.csproj \
  --startup-project src/Services/Library/Library.Api/Library.Api.csproj
```
#### 3. Run the REST API

From the solution root:
```
dotnet run --project src/Services/Library/Library.Api/Library.Api.csproj
```
The API will typically be available at:
- `http://localhost:5000`
- `https://localhost:7000`

Swagger UI is enabled in Development.

---

### gRPC Service

From the solution root:
```
dotnet run --project src/Services/Library/Library.gRPC/Library.gRPC.csproj
```
The gRPC service uses the contract defined in `Library.gRPC/Protos/library.proto`:

- `LibraryService.GetAllBooks(Empty) -> GetAllBooksResponse`
- `LibraryService.GetBookById(BookRequest) -> BookResponse`

Client stubs are generated into the `Library.gRPC` namespace.

---

### Running Tests

From the solution root:
```
dotnet test
```
Test categories:

- **Unit tests**: `Library.Tests/UnitTests/...`
- **Integration tests**: `Library.Tests/IntegrationTests/...`
  - Use a dedicated test database (see `appsettings.Testing.json` and `DatabaseFixture`).
- **Functional tests**: `Library.Tests/FunctionalTests/...`
  - Use `LibraryApiFactory` + `WebApplicationFactory<Program>`.
- **System tests**: `Library.Tests/SystemTests/...`
  - Exercise full flows (e.g., `BorrowReturnFlowTests`).

---

### License

This project is licensed under the MIT License. See `LICENSE` for details.
