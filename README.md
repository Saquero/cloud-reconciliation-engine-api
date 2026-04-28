# Cloud Reconciliation Engine API

A professional backend portfolio project built with **C#**, **.NET 8**, **Clean Architecture**, **Domain-Driven Design (DDD)** and **Hexagonal Architecture**.

This project models a real-world cloud/SaaS operations scenario: detecting mismatches between internal service records and external provider snapshots before they become billing or provisioning issues.

---

## Why this project matters

In B2B SaaS and cloud commerce platforms, billing accuracy and service provisioning consistency are critical.

A common operational risk is:

```text
Internal platform data != External provider data

Example:

Internal system:
Customer has 20 Microsoft 365 seats.

External provider snapshot:
Customer has 23 Microsoft 365 seats.

Result:
The system detects a reconciliation issue because billing or provisioning may be incorrect.

This project demonstrates how to model this kind of business problem with a maintainable backend architecture.

Tech Stack
C#
.NET 8
ASP.NET Core Web API
Entity Framework Core
SQLite
Swagger / OpenAPI
xUnit
NetArchTest
Architecture
src/
  CleanDddHexagonal.Domain          -> Business rules and domain model
  CleanDddHexagonal.Application     -> Use cases and application ports
  CleanDddHexagonal.Infrastructure  -> EF Core, SQLite and adapters
  CleanDddHexagonal.Api             -> HTTP API, controllers and middleware

tests/
  CleanDddHexagonal.Tests           -> Unit and architecture tests
Clean Architecture

The solution separates responsibilities into clear layers:

LayerResponsibility
DomainBusiness rules, entities, value objects, domain services and domain events
ApplicationUse cases, orchestration and ports
InfrastructureEF Core repositories, SQLite persistence, system clock and event dispatcher
APIHTTP controllers, Swagger and middleware

The dependency rule is protected:

API -> Application -> Domain
Infrastructure -> Application / Domain
Domain -> no external framework dependency

Controllers stay thin.
Business orchestration lives in use cases.
Business rules live in the domain.

Domain-Driven Design

The domain includes tactical DDD patterns:

DDD PatternImplementation
EntitiesCustomerAccount, InternalUsageRecord, ExternalUsageSnapshot, ReconciliationIssue
Value ObjectsCustomerName, ServiceSku, MoneyAmount
Domain ServiceUsageReconciliationService
Domain EventsUsageMismatchDetectedDomainEvent, ReconciliationIssueResolvedDomainEvent
Domain ExceptionsDomainException, InvalidDomainValueException, IssueAlreadyResolvedException
Repository PortsInterfaces such as IReconciliationIssueRepository

The domain models business rules such as:

Customer names cannot be empty.
Service SKUs are normalized.
Money values cannot be negative.
Seat count mismatches create reconciliation issues.
Cost mismatches create reconciliation issues.
Missing internal or external records create reconciliation issues.
Already resolved issues cannot be resolved again.
Hexagonal Architecture

This project follows Ports and Adapters principles:

ConceptImplementation
Inbound AdapterASP.NET Core Controllers
Application Use CasesRunReconciliationUseCase, ResolveIssueUseCase, etc.
Output PortsRepository interfaces
Output AdaptersEF Core repository implementations
System PortIDateTimeProvider
System AdapterSystemDateTimeProvider
Event PortIDomainEventDispatcher
Event AdapterLoggingDomainEventDispatcher

The domain does not know about:

ASP.NET Core
EF Core
SQLite
Swagger
Infrastructure details
Main Workflow
1. Create a customer
2. Register internal usage
3. Import external provider snapshot
4. Run reconciliation
5. Detect mismatches
6. Create reconciliation issues
7. Dispatch domain events
8. Resolve issues
API Endpoints
MethodEndpointDescription
GET/api/customersList customers
POST/api/customersCreate customer
POST/api/usage/internalRegister internal usage record
POST/api/usage/external-snapshotsImport external provider snapshot
POST/api/reconciliation/runRun reconciliation for a customer
GET/api/reconciliation/issues/openList open reconciliation issues
PATCH/api/reconciliation/issues/{issueId}/resolveResolve an issue
Example Demo Flow
1. Create customer
{
  "name": "Contoso Ltd"
}
2. Register internal usage
{
  "customerId": "CUSTOMER_ID_HERE",
  "provider": 1,
  "serviceSku": "MS-365-BUSINESS-PREMIUM",
  "seatCount": 20,
  "monthlyCost": 300,
  "currency": "EUR"
}
3. Import external provider snapshot
{
  "customerId": "CUSTOMER_ID_HERE",
  "provider": 1,
  "serviceSku": "MS-365-BUSINESS-PREMIUM",
  "seatCount": 23,
  "monthlyCost": 300,
  "currency": "EUR"
}
4. Run reconciliation
{
  "customerId": "CUSTOMER_ID_HERE"
}

Expected result:

Seat count mismatch detected: internal=20, external=23.
Run locally
git clone https://github.com/YOUR_USERNAME/cloud-reconciliation-engine-api.git
cd cloud-reconciliation-engine-api
dotnet restore
dotnet run --project src/CleanDddHexagonal.Api

Open Swagger using the URL shown in the terminal:

http://localhost:PORT/swagger
Run tests
dotnet test
Architecture Tests

The project includes architecture tests using NetArchTest to ensure:

Domain does not depend on Infrastructure.
Domain does not depend on API.
Domain does not depend on EF Core.
Application does not depend on Infrastructure.
Application does not depend on API.
Application does not depend on EF Core.

This helps prevent accidental coupling as the project grows.

What this project demonstrates
Clean Architecture in a real backend scenario.
DDD tactical patterns applied to a business domain.
Hexagonal Architecture using ports and adapters.
Domain events for business-relevant actions.
Repository abstractions to decouple persistence.
EF Core used only as an infrastructure adapter.
System time abstracted behind a port.
Thin controllers and focused use cases.
Automated architecture boundary tests.
A business case related to cloud operations, billing accuracy and provider data reconciliation.
Portfolio Summary

Built a .NET 8 Cloud Reconciliation Engine using Clean Architecture, DDD and Hexagonal Architecture to detect billing and provisioning mismatches between internal service records and external provider snapshots.

Implemented domain entities, value objects, domain services, domain events, application use cases, repository ports, infrastructure adapters and architecture tests to keep business logic independent from frameworks and persistence.

Future Improvements
Add PostgreSQL support.
Add Docker Compose.
Add provider-specific adapters.
Add scheduled background reconciliation.
Add audit trail.
Add authentication and authorization.
Add CI/CD with GitHub Actions.
