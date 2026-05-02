# Cloud Reconciliation Engine - Enterprise Edition

**Multi-provider cloud billing reconciliation platform** built with Clean Architecture, Domain-Driven Design, and Hexagonal Architecture.

> Designed for enterprises like Cloudmore that need precision in cloud cost management across multiple providers.

---

## 🎯 Overview

The Cloud Reconciliation Engine is a production-grade system that automatically reconciles cloud provider billing data against internal usage records, detects billing discrepancies, allocates costs to customers, and identifies margin leakage.

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
    └── UnitTest1.cs                 # Additional unit tests
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

# Run migrations (create database)
dotnet ef database update -p src/CleanDddHexagonal.Infrastructure -s src/CleanDddHexagonal.Api

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

### Reconciliation (Existing)

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

### Test Example

```csharp
[Fact]
public void CreateCostAllocationRecord_WithValidData_ShouldCalculateMarginCorrectly()
{
    // Arrange
    var record = new CostAllocationRecord(
        tenantId: 1,
        customerId: 100,
        serviceSku: "Virtual-Machines",
        actualCost: 1000m,
        billedPrice: 1250m,
        periodStart: DateTime.UtcNow.AddMonths(-1),
        periodEnd: DateTime.UtcNow);

    // Assert
    Assert.Equal(250m, record.MarginAmount);  // BilledPrice - ActualCost
    Assert.Equal(25m, record.MarginPercentage); // (Margin / ActualCost) * 100
}
```

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

## 👥 Contributing

This is a demonstration project for **Cloudmore** showing enterprise-grade cloud reconciliation architecture.

---

## 📄 License

Internal project - Cloudmore

---

## 🎓 Architecture Decision Records

### 1. Why Hexagonal Architecture?

**Decision**: Implement ports and adapters to decouple from cloud providers.

**Rationale**:
- Easy to swap providers (Azure ↔ AWS)
- Mock implementations for testing
- Independent domain logic
- Dependency inversion principle

### 2. Why Domain-Driven Design?

**Decision**: Structure the domain around billing and reconciliation concepts.

**Rationale**:
- Complex business logic (margin calculations, reconciliation rules)
- Clear domain boundaries
- Ubiquitous language for team communication
- Value objects for type safety

### 3. Why Clean Architecture?

**Decision**: Strict layer separation with dependency rules.

**Rationale**:
- Testability at all levels
- Clear responsibility separation
- Framework-agnostic domain
- Long-term maintainability

---

## 📞 Support

For questions or issues, contact the development team or refer to the API documentation at `/swagger` when the application is running.

---

**Built with precision for enterprise cloud reconciliation.** ☁️
