# Cloud Reconciliation Engine - Enterprise Edition

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-blueviolet)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Tests: Passing](https://img.shields.io/badge/Tests-14%2F14%20Passing-brightgreen)](./tests)
[![Architecture: Clean](https://img.shields.io/badge/Architecture-Clean%20%2B%20DDD%20%2B%20Hexagonal-blue)](#-architecture)

**Multi-provider cloud billing reconciliation platform** built with Clean Architecture, Domain-Driven Design, and Hexagonal Architecture.

> A production-grade system for enterprises that need precision in cloud cost management across multiple providers.

---

## ?? Quick Demo

``ash
# Clone
git clone https://github.com/Saquero/cloud-reconciliation-engine-api.git

# Run
dotnet run --project src/CleanDddHexagonal.Api

# Open Swagger
http://localhost:5214/swagger
``

---

## ? Features

- **Multi-Provider Support** - Azure, AWS (extensible to GCP)
- **Real-Time Reconciliation** - Automated mismatch detection
- **Cost Allocation** - Distribute costs across customers  
- **Margin Analysis** - Identify profitability
- **Multi-Tenant** - Enterprise-grade isolation
- **14/14 Tests Passing** - Full domain logic coverage
- **Clean Architecture** - DDD, Hexagonal, fully testable

---

## ??? Architecture

### Layered Design

`
+-----------------------------------------+
¦         API Layer (REST)                ¦
¦    (Controllers, Middleware)            ¦
+-----------------------------------------¦
¦    Application Layer (Use Cases)        ¦
¦  (Business Workflows, DTOs, Ports)      ¦
+-----------------------------------------¦
¦      Domain Layer (Business Logic)      ¦
¦ (Entities, Value Objects, Services)     ¦
+-----------------------------------------¦
¦  Infrastructure Layer (Adapters, DB)    ¦
¦ (EF Core, Cloud Providers, Repositories)¦
+-----------------------------------------+
`

**See [ARCHITECTURE.md](./ARCHITECTURE.md) for detailed design documentation.**

---

## ?? Project Structure

``
src/
+-- CleanDddHexagonal.Domain/          # Business logic
+-- CleanDddHexagonal.Application/     # Use cases & workflows
+-- CleanDddHexagonal.Infrastructure/  # Data access & adapters
+-- CleanDddHexagonal.Api/             # REST endpoints

tests/
+-- CleanDddHexagonal.Tests/           # 14 unit tests
``

---

## ?? Getting Started

### Prerequisites

- .NET 8.0 SDK
- Git

### Build & Run

``ash
# Restore dependencies
dotnet restore

# Build
dotnet build

# Run tests
dotnet test

# Run API
dotnet run --project src/CleanDddHexagonal.Api --urls "http://localhost:5214"
``

### API Documentation

Open Swagger UI at: **http://localhost:5214/swagger**

---

## ?? API Endpoints

### Provider Management
- POST /api/v1/providers/register - Register cloud provider
- GET /api/v1/providers/{providerId} - Get provider
- DELETE /api/v1/providers/{providerId} - Deactivate provider

### Cost Allocation  
- GET /api/v1/costallocation/report/{tenantId} - Cost report
- GET /api/v1/costallocation/margin-analysis/{customerId} - Margin analysis
- POST /api/v1/costallocation/sync/{providerId} - Sync costs

### Reconciliation
- POST /api/reconciliation/run - Run reconciliation
- GET /api/reconciliation/issues/open - Get issues
- PATCH /api/reconciliation/issues/{id}/resolve - Resolve issue

---

## ?? Cloud Providers

### Azure
- Status: MVP (mock data)
- Adapter: AzureUsageClientMock
- Next: Real Azure Cost Management API

### AWS  
- Status: MVP (mock data)
- Adapter: AWSCostExplorerClientMock
- Next: Real AWS Cost Explorer API

---

## ?? Testing

- **14/14 tests passing**
- Domain logic validation
- Architecture compliance
- Provider management lifecycle

``ash
dotnet test
``

---

## ??? Technology Stack

| Component | Technology |
|-----------|-----------|
| Language | C# (.NET 8) |
| API | ASP.NET Core |
| ORM | Entity Framework Core |
| Database | SQLite / PostgreSQL |
| Validation | FluentValidation |
| Testing | xUnit |

---

## ?? Design Patterns

- **Clean Architecture** - Separation of concerns
- **Domain-Driven Design (DDD)** - Business-focused modeling
- **Hexagonal Architecture** - Port & Adapter pattern
- **Repository Pattern** - Data abstraction
- **Use Cases** - Business workflows
- **Value Objects** - Type-safe domain values

---

## ??? Roadmap

### Phase 1: MVP ?
- [x] Multi-provider architecture
- [x] Azure + AWS adapters
- [x] Cost allocation & margin analysis
- [x] 14 unit tests
- [x] REST API with Swagger

### Phase 2: Production
- [ ] Real Azure Cost Management API
- [ ] Real AWS Cost Explorer API  
- [ ] Provider credential encryption
- [ ] Advanced reconciliation rules

### Phase 3: Enterprise
- [ ] Google Cloud support
- [ ] Custom pricing rules
- [ ] Advanced dashboards
- [ ] Anomaly detection

---

## ?? Documentation

- **[ARCHITECTURE.md](./ARCHITECTURE.md)** - Detailed architecture guide
- **[README.md](./README.md)** - This file
- **Swagger UI** - Interactive API docs at /swagger

---

## ? Show Your Support

If this project helps you:

- **Give it a star** ? to show support
- **Use it as a base** for your projects  
- **Connect with me** ?? on LinkedIn

### ?? Contact

?? **Created by** ? **[Manu Saquero](https://www.linkedin.com/in/manusaquero/)**

?? **Software Developer** ?? Passionate about building real products

---

**Built with precision for enterprise cloud reconciliation.** ??
