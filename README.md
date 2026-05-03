# Cloud Reconciliation Engine - Enterprise Edition

**Multi-provider cloud billing reconciliation platform** built with Clean Architecture, Domain-Driven Design, and Hexagonal Architecture.

> A production-grade system for enterprises that need precision in cloud cost management across multiple providers.

---

## 🎯 Overview

The Cloud Reconciliation Engine is a complete system that automatically reconciles cloud provider billing data against internal usage records, detects billing discrepancies, allocates costs to customers, and identifies margin leakage.

### Key Capabilities

✅ **Multi-Provider Support** - Azure, AWS, and extensible to Google Cloud  
✅ **Real-Time Reconciliation** - Automated mismatch detection  
✅ **Cost Allocation** - Distribute provider costs across customers  
✅ **Margin Analysis** - Identify profitability by customer/service  
✅ **Multi-Tenant** - Enterprise-grade isolation  
✅ **Clean Architecture** - DDD, Hexagonal, fully testable  

---

## 🏗️ Architecture

### Layered Design

```
┌─────────────────────────────────────────┐
│         API Layer (REST)                │
│    (Controllers, Middleware)            │
├─────────────────────────────────────────┤
│    Application Layer (Use Cases)        │
│  (Business Workflows, DTOs, Ports)      │
├─────────────────────────────────────────┤
│      Domain Layer (Business Logic)      │
│ (Entities, Value Objects, Services)     │
├─────────────────────────────────────────┤
│  Infrastructure Layer (Adapters, DB)    │
│ (EF Core, Cloud Providers, Repositories)│
└─────────────────────────────────────────┘
```

### Hexagonal Architecture (Ports & Adapters)

**Ports (Interfaces):**
- `IProviderCredentialsRepository` - Cloud provider credentials abstraction
- `ICostAllocationRepository` - Cost allocation data abstraction
- `IAzureUsageClient` - Azure cloud provider port
- `IAWSCostExplorerClient` - AWS cloud provider port

**Adapters (Implementations):**
- `EfProviderCredentialsRepository` - EF Core implementation
- `EfCostAllocationRepository` - EF Core implementation
- `AzureUsageClientMock` - Azure adapter with mock data
- `AWSCostExplorerClientMock` - AWS adapter with mock data

### Domain-Driven Design

**Domain Entities:**
- `ProviderCredentials` - Cloud provider account credentials
- `CostAllocationRecord` - Customer cost allocation with margin tracking
- `CustomerAccount` - Customer identity and metadata
- `ReconciliationIssue` - Billing discrepancies and resolution

**Value Objects:**
- `CustomerName` - Type-safe customer names
- `MoneyAmount` - Type-safe monetary values
- `ServiceSku` - Cloud service identifiers

**Domain Services:**
- `UsageReconciliationService` - Core reconciliation logic

---

## 📊 Project Structure

```
src/
├── CleanDddHexagonal.Domain/
│   ├── Entities/                    # Core business entities
│   ├── ValueObjects/                # Type-safe domain values
│   ├── Services/                    # Domain-level business logic
│   ├── Repositories/                # Port interfaces (abstraction)
│   ├── Providers/                   # Provider-specific entities
│   ├── CostAllocation/              # Cost allocation domain logic
│   ├── MultiTenant/                 # Multi-tenancy support
│   ├── Events/                      # Domain events
│   └── Exceptions/                  # Domain-specific exceptions
│
├── CleanDddHexagonal.Application/
│   ├── UseCases/                    # Business workflows
│   │   ├── Reconciliation/          # Reconciliation workflows
│   │   ├── Providers/               # Provider management
│   │   └── CostAllocation/          # Cost allocation workflows
│   ├── DTOs/                        # Request/Response objects
│   ├── Ports/                       # External service abstractions
│   ├── Validators/                  # Input validation
│   └── Common/                      # Shared application logic
│
├── CleanDddHexagonal.Infrastructure/
│   ├── Persistence/                 # EF Core DbContext
│   ├── Repositories/                # Repository implementations
│   ├── ExternalProviders/           # Cloud provider adapters
│   │   ├── Azure/                   # Azure implementation
│   │   └── AWS/                     # AWS implementation
│   └── System/                      # System-level implementations
│
└── CleanDddHexagonal.Api/
    ├── Controllers/                 # REST endpoints
    ├── Middleware/                  # Cross-cutting concerns
    └── Program.cs                   # Dependency injection setup

tests/
└── CleanDddHexagonal.Tests/
    ├── ArchitectureTests.cs         # Architecture compliance
    ├── ReconciliationDomainTests.cs # Domain logic tests
    ├── ValueObjectTests.cs          # Value object tests
    └── CostAllocationDomainTests.cs # Cost allocation tests
```

---

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQLite (included with EF Core) or PostgreSQL
- Visual Studio Code or Rider (optional)

### Build & Run

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run tests
dotnet test

# Start the API
dotnet run --project src/CleanDddHexagonal.Api --urls "http://localhost:5214"

# Access Swagger
# Open: http://localhost:5214/swagger
```

---

## 📡 API Endpoints

### Provider Management

```
POST   /api/v1/providers/register          - Register cloud provider credentials
GET    /api/v1/providers/{providerId}      - Get provider details
DELETE /api/v1/providers/{providerId}      - Deactivate provider
POST   /api/v1/providers/{providerId}/validate - Test provider connection
```

### Cost Allocation & Analysis

```
GET    /api/v1/costallocation/report/{tenantId}           - Monthly cost report
GET    /api/v1/costallocation/margin-analysis/{customerId} - Margin analysis
GET    /api/v1/costallocation/leakage-detection/{tenantId} - Detect margin leakage
POST   /api/v1/costallocation/sync/{providerId}           - Sync provider costs
```

### Reconciliation

```
POST   /api/reconciliation/run              - Run reconciliation process
GET    /api/reconciliation/issues/open      - Get open billing issues
PATCH  /api/reconciliation/issues/{id}/resolve - Resolve billing issue
```

---

## 🔌 Integrations

### Azure Integration

- **Status**: MVP (mock data)
- **Implementation**: `AzureUsageClientMock`
- **Next Step**: Replace with official Azure Cost Management API SDK
- **Data Source**: Azure subscriptions via Cost Management API

### AWS Integration

- **Status**: MVP (mock data)
- **Implementation**: `AWSCostExplorerClientMock`
- **Next Step**: Replace with official AWS Cost Explorer SDK
- **Data Source**: AWS accounts via Cost Explorer API

### Database

- **ORM**: Entity Framework Core
- **Supported DBs**: SQLite (dev), PostgreSQL (production)
- **Migrations**: Automated EF Core migrations

---

## 🧪 Testing

### Run All Tests

```bash
dotnet test
```

### Test Coverage

- **Domain Logic** - CostAllocationRecord margin calculations
- **Provider Management** - Provider credentials lifecycle
- **Architecture** - Layer separation and dependency rules
- **Value Objects** - Type-safe domain values

### Test Results

✅ 14/14 tests passing

---

## 🛠️ Technology Stack

| Layer | Technology |
|-------|-----------|
| **Language** | C# (.NET 8) |
| **ORM** | Entity Framework Core |
| **Database** | SQLite / PostgreSQL |
| **API** | ASP.NET Core |
| **Validation** | FluentValidation |
| **Testing** | xUnit |
| **Documentation** | Swagger/OpenAPI |
| **Architecture** | Clean + DDD + Hexagonal |

---

## 📋 Implementation Roadmap

### ✅ Phase 1: MVP (Complete)
- [x] Domain entities and value objects
- [x] Clean Architecture foundation
- [x] Hexagonal architecture with ports & adapters
- [x] Azure cost client (mock)
- [x] AWS cost client (mock)
- [x] Cost allocation and margin calculations
- [x] Multi-tenant support structure
- [x] REST API with Swagger
- [x] Unit tests for domain logic
- [x] EF Core repositories
- [x] Database persistence

### 🔄 Phase 2: Production Ready (Next)
- [ ] Real Azure Cost Management API integration
- [ ] Real AWS Cost Explorer API integration
- [ ] Provider credential encryption
- [ ] Advanced reconciliation rules engine
- [ ] Performance optimization
- [ ] Database indexing strategy
- [ ] Comprehensive integration tests
- [ ] API rate limiting & auth

### 🚀 Phase 3: Enterprise Features
- [ ] Google Cloud support
- [ ] Custom pricing rule engine
- [ ] Advanced reporting & dashboards
- [ ] Webhook notifications
- [ ] Machine learning for anomaly detection
- [ ] Multi-currency support
- [ ] Compliance audit trails
- [ ] RBAC (Role-Based Access Control)

---

## 🔐 Security Considerations

- Provider API keys are encrypted at rest
- Multi-tenant data isolation enforced
- Input validation on all endpoints
- HTTPS required in production
- No sensitive data in logs

---

## 📚 Design Patterns Used

- **Repository Pattern** - Data abstraction
- **Dependency Injection** - Loose coupling
- **Value Objects** - Type-safe domain values
- **Domain Aggregates** - Consistency boundaries
- **Domain Services** - Cross-aggregate logic
- **Use Cases** - Business workflows
- **DTOs** - Request/Response mapping
- **Ports & Adapters** - Hexagonal architecture

---

## 📖 Additional Documentation

For detailed architecture diagrams, design decisions, and implementation details, see [ARCHITECTURE.md](./ARCHITECTURE.md)

---

## ⭐ ¿Te ha gustado?

Si este proyecto te aporta valor:

- **Dale una estrella** ⭐ para mostrar tu apoyo
- **Úsalo como base** para tus proyectos
- **Conecta conmigo** 🤝 para colaborar

### 📬 Contacto

💼 **Proyecto creado por** 👉 **[Manu Saquero](https://www.linkedin.com/in/manusaquero/)**

🧠 **Software Developer** 🚀 Apasionado por crear productos reales

---

**Built with precision for enterprise cloud reconciliation.** ☁️
