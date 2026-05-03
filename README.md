# Cloud Reconciliation Engine — Enterprise Edition

<p align="center">
  <b>Multi-provider cloud billing reconciliation platform</b><br/>
  Built with Clean Architecture · DDD · Hexagonal
</p>

<p align="center">
  <img src="https://img.shields.io/badge/License-MIT-yellow.svg"/>
  <img src="https://img.shields.io/badge/.NET-8.0-blueviolet"/>
  <img src="https://img.shields.io/badge/Tests-14%2F14%20Passing-brightgreen"/>
  <img src="https://img.shields.io/badge/Architecture-Clean%20%2B%20DDD%20%2B%20Hexagonal-blue"/>
</p>

---

## 🚀 Quick Start

```bash
git clone https://github.com/Saquero/cloud-reconciliation-engine-api.git
cd cloud-reconciliation-engine-api
dotnet restore
dotnet run --project src/CleanDddHexagonal.Api --urls "http://localhost:5214"
```

👉 API: http://localhost:5214  
👉 Swagger: http://localhost:5214/swagger  

---

## ✨ Features

- Multi-provider architecture (Azure, AWS, extensible to GCP)
- Real-time reconciliation with mismatch detection
- Cost allocation per tenant/customer
- Margin analysis for profitability
- Multi-tenant ready design
- Fully tested domain (14/14 tests)
- Clean Architecture (DDD + Hexagonal)

---

## 🧠 Architecture

```text
API Layer (REST)
  └── Controllers / Middleware

Application Layer
  └── Use Cases / DTOs / Ports

Domain Layer
  └── Entities / Value Objects / Services

Infrastructure Layer
  └── EF Core / Providers / Repositories
```

📄 Full details → [ARCHITECTURE.md](./ARCHITECTURE.md)

---

## 📁 Project Structure

```bash
src/
├── CleanDddHexagonal.Domain/
├── CleanDddHexagonal.Application/
├── CleanDddHexagonal.Infrastructure/
└── CleanDddHexagonal.Api/

tests/
└── CleanDddHexagonal.Tests/
```

---

## ⚙️ Run Locally

```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project src/CleanDddHexagonal.Api --urls "http://localhost:5214"
```

---

## 📡 API Overview

### Providers
- POST `/api/v1/providers/register`
- GET `/api/v1/providers/{id}`
- DELETE `/api/v1/providers/{id}`

### Cost Allocation
- GET `/api/v1/costallocation/report/{tenantId}`
- GET `/api/v1/costallocation/margin-analysis/{customerId}`
- POST `/api/v1/costallocation/sync/{providerId}`

### Reconciliation
- POST `/api/reconciliation/run`
- GET `/api/reconciliation/issues/open`
- PATCH `/api/reconciliation/issues/{id}/resolve`

---

## ☁️ Cloud Providers

**Azure**
- Mock implementation (MVP)
- Next → Azure Cost Management API

**AWS**
- Mock implementation (MVP)
- Next → AWS Cost Explorer API

---

## 🧪 Testing

- 14/14 tests passing  
- Domain logic validated  
- Architecture compliance  

```bash
dotnet test
```

---

## 🧰 Tech Stack

| Layer | Tech |
|------|------|
| Language | C# (.NET 8) |
| API | ASP.NET Core |
| ORM | Entity Framework Core |
| DB | SQLite / PostgreSQL |
| Validation | FluentValidation |
| Testing | xUnit |

---

## 🧩 Design

- Clean Architecture
- Domain-Driven Design (DDD)
- Hexagonal (Ports & Adapters)
- Repository Pattern
- Use Cases
- Value Objects

---

## 🎯 Roadmap

**MVP**
- ✔ Multi-provider
- ✔ Azure + AWS
- ✔ Cost allocation
- ✔ Tests
- ✔ Swagger API

**Production**
- ☐ Real Azure API  
- ☐ Real AWS API  
- ☐ Credential security  
- ☐ Advanced rules  

**Enterprise**
- ☐ GCP support  
- ☐ Custom pricing  
- ☐ Dashboards  
- ☐ Anomaly detection  

---

## 📚 Docs

- 📄 [Architecture](./ARCHITECTURE.md)
- 📘 Swagger → `/swagger`


---

## ⭐ ¿Te ha gustado?

Si este proyecto te aporta valor:

- Dale una estrella ⭐  
- Úsalo como base  
- Conecta conmigo 🤝
---

## 📬 Contacto

💼 Proyecto creado por 👉 [**Manu Saquero**](https://www.linkedin.com/in/manusaquero/)  

🧠 Software Developer  
🚀 Apasionado por crear productos reales
