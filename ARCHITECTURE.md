# Architecture Documentation - Cloud Reconciliation Engine

## Table of Contents

1. [Overview](#overview)
2. [Architectural Patterns](#architectural-patterns)
3. [Layer Descriptions](#layer-descriptions)
4. [Design Decisions](#design-decisions)
5. [Data Flow](#data-flow)
6. [Testing Strategy](#testing-strategy)
7. [Deployment](#deployment)

---

## Overview

The Cloud Reconciliation Engine is built on three complementary architectural patterns:

1. **Clean Architecture** - Ensures separation of concerns and testability
2. **Domain-Driven Design (DDD)** - Centers business logic in the domain layer
3. **Hexagonal Architecture** - Decouples external dependencies through ports and adapters

This combination creates a system that is:
- **Testable** - All business logic can be tested in isolation
- **Maintainable** - Clear separation of responsibilities
- **Scalable** - Easy to add new providers or features
- **Flexible** - External dependencies are abstractions, not implementations

---

## Architectural Patterns

### 1. Clean Architecture

Clean Architecture ensures that the code organization reflects the importance of the business logic.

```
┌─────────────────────────────────────────┐
│         Frameworks & Drivers            │
│    (ASP.NET Core, Entity Framework)     │
├─────────────────────────────────────────┤
│    Interface Adapters (Controllers)     │
│          (REST API, Swagger)            │
├─────────────────────────────────────────┤
│     Application Business Rules          │
│  (Use Cases, DTOs, Service Boundaries)  │
├─────────────────────────────────────────┤
│     Enterprise Business Rules           │
│  (Entities, Value Objects, Services)    │
└─────────────────────────────────────────┘
```

**Key Principle**: Dependencies point inward. The domain layer knows nothing about outer layers.

**Benefits**:
- Independent of frameworks
- Testable without external dependencies
- Independent of UI
- Independent of database
- Independent of external services

### 2. Domain-Driven Design (DDD)

DDD structures the codebase around the business domain, using a shared vocabulary between developers and stakeholders.

**Core Concepts**:

**Entities** - Objects with identity that persist over time
```csharp
public class ProviderCredentials
{
    public int Id { get; private set; }
    public int TenantId { get; private set; }
    public CloudProvider Provider { get; private set; }
    public DateTime CreatedAt { get; private set; }
    // Business methods...
}
```

**Value Objects** - Objects without identity, defined by their attributes
```csharp
public class MoneyAmount
{
    public decimal Value { get; }
    public string Currency { get; }
    // No Id, immutable
}
```

**Aggregates** - Clusters of entities and value objects treated as a single unit
```csharp
// ProviderCredentials is the aggregate root
// Controls access to related objects
```

**Domain Services** - Stateless services that express domain logic
```csharp
public class UsageReconciliationService
{
    public ReconciliationResult Reconcile(
        InternalUsageRecord internal,
        ExternalUsageSnapshot external)
    {
        // Domain-level business logic
    }
}
```

**Domain Events** - Changes that are significant to the business
```csharp
public class UsageMismatchDetectedDomainEvent : IDomainEvent
{
    public ReconciliationIssue Issue { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### 3. Hexagonal Architecture (Ports & Adapters)

Hexagonal Architecture decouples the application from external systems through well-defined boundaries.

```
                    ┌─────────────────────┐
                    │    Controllers      │
                    │  (Primary Adapters) │
                    └──────────┬──────────┘
                               │
┌──────────────────────────────┴──────────────────────────────┐
│                                                              │
│              Application Business Rules                     │
│              (Use Cases, Services)                          │
│                                                              │
└──────────────────────────────┬──────────────────────────────┘
         ▲                      │
         │                      │
    ┌────┴─────────────────┬────┴─────────────────┐
    │                      │                      │
┌───┴─────┐          ┌────┴─────┐          ┌────┴─────┐
│ Azure   │          │  AWS     │          │ Database │
│Adapter  │          │ Adapter  │          │ Adapter  │
└─────────┘          └──────────┘          └──────────┘
 (Secondary Adapters)
```

**Ports** (Interfaces in Application/Domain layers):
- `IAzureUsageClient` - Defines how to interact with Azure
- `IProviderCredentialsRepository` - Defines how to store credentials
- `ICostAllocationRepository` - Defines how to store allocations

**Adapters** (Implementations in Infrastructure layer):
- `AzureUsageClientMock` - Concrete Azure implementation
- `EfProviderCredentialsRepository` - EF Core implementation
- `EfCostAllocationRepository` - EF Core implementation

---

## Layer Descriptions

### 1. Domain Layer (`CleanDddHexagonal.Domain`)

**Responsibility**: Contains the core business logic, independent of any framework or external system.

**Components**:

- **Entities** (`Entities/`)
  - `CustomerAccount` - Represents a customer
  - `InternalUsageRecord` - Internal usage data
  - `ExternalUsageSnapshot` - Provider billing data
  - `ReconciliationIssue` - Detected billing discrepancies
  - `ProviderCredentials` - Cloud provider authentication
  - `CostAllocationRecord` - Cost distribution to customers

- **Value Objects** (`ValueObjects/`)
  - `CustomerName` - Type-safe customer name
  - `MoneyAmount` - Type-safe monetary value
  - `ServiceSku` - Cloud service identifier

- **Domain Services** (`Services/`)
  - `UsageReconciliationService` - Core reconciliation logic

- **Repositories** (`Repositories/`) - Port interfaces only
  - `ICustomerAccountRepository`
  - `IReconciliationIssueRepository`
  - `IUsageRecordRepository`
  - `IProviderCredentialsRepository` - NEW
  - `ICostAllocationRepository` - NEW

- **Enums** (`Enums/`)
  - `CloudProvider` - Azure, AWS, GoogleCloud, Custom
  - `ReconciliationIssueType` - Type of billing issue
  - `ReconciliationIssueStatus` - Open, Resolved

- **Events** (`Events/`)
  - `UsageMismatchDetectedDomainEvent`
  - `ReconciliationIssueResolvedDomainEvent`

- **Exceptions** (`Exceptions/`)
  - Custom domain exceptions

**Key Rule**: No dependencies on outer layers. Domain is completely decoupled from infrastructure.

### 2. Application Layer (`CleanDddHexagonal.Application`)

**Responsibility**: Orchestrates the domain layer to fulfill user requests. Contains business rules about how use cases work.

**Components**:

- **Use Cases** (`UseCases/`)
  - `ReconciliationUseCase` - Run reconciliation
  - `RegisterInternalUsageUseCase` - Record internal usage
  - `ImportExternalUsageSnapshotUseCase` - Import provider data
  - `RegisterProviderCredentialsUseCase` - NEW
  - `SyncAzureCostsUseCase` - NEW
  - `CalculateCostAllocationUseCase` - NEW

- **DTOs** (`DTOs/`)
  - Request DTOs for input validation
  - Response DTOs for output formatting
  - `RegisterProviderCredentialsRequest`
  - `CostAllocationReportDto`
  - `SyncResultDto`

- **Ports** (`Ports/`)
  - `IDateTimeProvider` - Abstracts system time
  - `IDomainEventDispatcher` - Publishes domain events
  - `IAzureUsageClient` - NEW - Azure provider port
  - `IAWSCostExplorerClient` - NEW - AWS provider port

- **Validators** (`Validators/`)
  - FluentValidation rules

- **Common** (`Common/`)
  - `Result<T>` - Success/failure pattern

**Key Rule**: Uses interfaces (ports) to communicate with infrastructure. Dependency injection injected at startup.

### 3. Infrastructure Layer (`CleanDddHexagonal.Infrastructure`)

**Responsibility**: Implements the ports defined by the application/domain layers. Contains all framework-specific code.

**Components**:

- **Persistence** (`Persistence/`)
  - `AppDbContext` - EF Core database context
  - Database configuration and migrations

- **Repositories** (`Repositories/`)
  - `EfCustomerAccountRepository` - EF Core implementation
  - `EfReconciliationIssueRepository` - EF Core implementation
  - `EfUsageRecordRepository` - EF Core implementation
  - `EfProviderCredentialsRepository` - NEW
  - `EfCostAllocationRepository` - NEW

- **External Providers** (`ExternalProviders/`)
  - **Azure/** - Azure-specific code
    - `AzureUsageClientMock` - Azure adapter
    - Azure SDK integration (when moving from mock)
  - **AWS/** - AWS-specific code
    - `AWSCostExplorerClientMock` - NEW - AWS adapter
    - AWS SDK integration (when moving from mock)

- **System** (`System/`)
  - `SystemDateTimeProvider` - System time implementation
  - `LoggingDomainEventDispatcher` - Event dispatcher

**Key Rule**: Implements interfaces defined by domain/application. No domain logic here.

### 4. API Layer (`CleanDddHexagonal.Api`)

**Responsibility**: Exposes the application through REST endpoints. Handles HTTP concerns.

**Components**:

- **Controllers** (`Controllers/`)
  - `CustomersController` - Customer endpoints
  - `ReconciliationController` - Reconciliation endpoints
  - `UsageRecordsController` - Usage endpoints
  - `ProvidersController` - NEW - Provider management
  - `CostAllocationController` - NEW - Cost analysis

- **Middleware** (`Middleware/`)
  - `GlobalExceptionMiddleware` - Error handling

- **Program.cs**
  - Dependency Injection setup
  - Service registration
  - Pipeline configuration

**Key Rule**: Thin controllers. All business logic in domain/application layers.

### 5. Testing Layer (`CleanDddHexagonal.Tests`)

**Responsibility**: Validates domain and application logic.

**Tests**:

- `ArchitectureTests.cs` - Validates layer dependencies
- `ReconciliationDomainTests.cs` - Domain logic validation
- `ValueObjectTests.cs` - Value object behavior
- `CostAllocationDomainTests.cs` - NEW - Margin calculations

---

## Design Decisions

### 1. Why Hexagonal Architecture?

**Problem**: Needed to support multiple cloud providers (Azure, AWS) without tight coupling.

**Solution**: Ports (interfaces) define provider contracts. Adapters (implementations) handle provider-specific logic.

**Benefit**: Easy to add new providers by creating new adapters.

### 2. Why Domain-Driven Design?

**Problem**: Billing reconciliation has complex business rules that are difficult to express in code.

**Solution**: Structure code around domain concepts (Provider, CostAllocation, Reconciliation).

**Benefit**: Code reflects business terminology. Easier to collaborate with business stakeholders.

### 3. Why Repository Pattern?

**Problem**: Database queries scattered across codebase, hard to change database.

**Solution**: Repositories abstract data access behind interfaces.

**Benefit**: Database implementation can change without affecting domain/application logic.

### 4. Why Value Objects?

**Problem**: Money amounts, SKUs, customer names scattered as strings/decimals throughout code.

**Solution**: Wrap in type-safe Value Objects with validation.

**Benefit**: Type safety, validation in one place, semantic meaning in code.

### 5. Why Use Cases?

**Problem**: Business logic could end up in controllers or services without clear boundaries.

**Solution**: Each use case = one business workflow (Register Provider, Run Reconciliation, etc).

**Benefit**: Testable workflows, clear responsibilities, reusable logic.

---

## Data Flow

### Provider Registration Flow

```
1. Client sends POST /api/v1/providers/register
                    ↓
2. ProvidersController receives request
                    ↓
3. Validates request using FluentValidation
                    ↓
4. Calls RegisterProviderCredentialsUseCase
                    ↓
5. UseCase creates ProviderCredentials entity
                    ↓
6. Calls IProviderCredentialsRepository.AddAsync()
                    ↓
7. EfProviderCredentialsRepository saves to database
                    ↓
8. Returns ProviderCredentialsDto to client
```

### Cost Allocation Flow

```
1. Trigger: SyncAzureCostsUseCase
                    ↓
2. Calls IAzureUsageClient.GetUsageDataAsync()
                    ↓
3. AzureUsageClientMock returns mock cost data
                    ↓
4. For each cost record:
   - Create CostAllocationRecord
   - Mark up price by 25%
   - Calculate margin
                    ↓
5. Save records via ICostAllocationRepository
                    ↓
6. EfCostAllocationRepository persists to database
                    ↓
7. Return sync summary
```

---

## Testing Strategy

### Unit Tests (Domain Logic)

Test domain entities in isolation:

```csharp
[Fact]
public void CreateCostAllocationRecord_ShouldCalculateMargin()
{
    var record = new CostAllocationRecord(
        tenantId: 1,
        customerId: 100,
        serviceSku: "Virtual-Machines",
        actualCost: 1000m,
        billedPrice: 1250m,
        periodStart: DateTime.UtcNow.AddMonths(-1),
        periodEnd: DateTime.UtcNow);

    Assert.Equal(250m, record.MarginAmount);
    Assert.Equal(25m, record.MarginPercentage);
}
```

### Integration Tests (Coming Soon)

Test use cases with real database:

```csharp
[Fact]
public async Task SyncAzureCosts_ShouldPersistToDatabase()
{
    // Arrange: Setup real DbContext
    // Act: Run sync
    // Assert: Verify database records
}
```

### Architecture Tests

Verify layer boundaries:

```csharp
[Fact]
public void Application_ShouldNotDependOnInfrastructure()
{
    // Verify no Infrastructure references in Application
}
```

---

## Deployment

### Development

```bash
dotnet run --project src/CleanDddHexagonal.Api
```

Database: SQLite (auto-created)

### Staging

```bash
dotnet publish -c Release
# Deploy to container or IIS
# Database: PostgreSQL
```

### Production

```bash
# Build container
docker build -t cloud-reconciliation-engine .

# Deploy with PostgreSQL
# Enable HTTPS
# Configure monitoring
# Setup CI/CD pipeline
```

---

## Future Enhancements

### Phase 2: Real Provider Integration

Replace mock adapters with real Azure and AWS SDKs:

```csharp
// Current (Mock)
public class AzureUsageClientMock : IAzureUsageClient { }

// Future (Real)
public class AzureUsageClient : IAzureUsageClient
{
    private readonly CostManagementClient _client;
    // Real Azure Cost Management API integration
}
```

### Phase 3: Advanced Features

- Custom pricing rules engine
- Machine learning anomaly detection
- Advanced reporting dashboards
- RBAC and audit trails

---

## Summary

This architecture creates a system that is:

| Aspect | How Achieved |
|--------|-------------|
| **Testable** | Dependency injection, ports & adapters, value objects |
| **Maintainable** | Clear layer separation, DDD domain language |
| **Scalable** | Provider-agnostic ports, new adapters for new sources |
| **Flexible** | Easy to swap implementations (Azure ↔ AWS ↔ Real APIs) |
| **Professional** | Enterprise patterns, comprehensive testing, documentation |

The combination of Clean Architecture, DDD, and Hexagonal patterns creates a codebase that is easy to understand, modify, and extend over time.

---

**For questions or clarifications, refer to the main README.md or explore the source code.**
