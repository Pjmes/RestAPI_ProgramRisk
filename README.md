# Program Risk REST API

An ASP.NET Core Web API that models program risk items and exposes structured HTTP endpoints. Built as part of a short series bridging Program Management and software engineering fundamentals.

## What This Is

This project is the natural next step after a Razor Pages Change Tracker — instead of returning HTML to a browser, this API returns structured JSON data. That shift reflects how real systems communicate: services talking to services, not servers talking to users.

As a Program Manager, risk management is a daily workflow. This project translates that workflow into a deliberately designed API with controlled state transitions, clear contracts, and structured response signaling.

## Architecture

```
ProgramRiskAPI/
├── Controllers/        # HTTP request handling only — no business logic
├── Models/
│   ├── Enums/          # RiskStatus, RiskProbability, RiskImpact
│   ├── DTOs/           # What enters and exits the API (CreateRiskDto, RiskResponseDto)
│   └── Risk.cs         # Internal domain model
├── Services/           # Business logic lives here (IRiskService, RiskService)
└── Program.cs
```

## Endpoints

| Method | Route | Description |
|---|---|---|
| `GET` | `/api/risks` | List all active risks |
| `GET` | `/api/risks/{id}` | Get a single risk by ID |
| `POST` | `/api/risks` | Create a new risk |
| `PATCH` | `/api/risks/{id}/status` | Update status only (controlled transition) |
| `DELETE` | `/api/risks/{id}` | Soft delete — record is flagged, not removed |

## Risk Model

Each risk item carries:

- **Title / Description / Owner / ProgramId**
- **Probability** — `Low (1)`, `Medium (2)`, `High (3)`
- **Impact** — `Low (1)`, `Medium (2)`, `High (3)`
- **RiskScore** — calculated automatically as `Probability × Impact` (max: 9)
- **Status** — `Identified → Open → Mitigated / Accepted → Closed`
- **Timestamps** — `CreatedAt`, `UpdatedAt`
- **IsDeleted** — soft delete flag for auditability

## Key Design Decisions

**DTOs over raw models** — Callers never see internal fields like `IsDeleted`. The DTO is the contract; the domain model is the implementation detail.

**PATCH for status only** — A status transition is not a full record update. Separating it prevents callers from accidentally overwriting fields while moving a workflow state.

**Enum-driven scoring** — `RiskScore` is a computed property, not a stored value. There's no way for it to drift out of sync.

**Soft delete** — Risks are never permanently removed. This preserves audit history, which matters in any compliance-adjacent program context.

**In-memory store** — No database setup required to run. The service interface (`IRiskService`) means swapping in Entity Framework Core later requires no changes to the controller.

## Running Locally

```bash
dotnet restore
dotnet run
```

Navigate to `https://localhost:{port}/swagger` for the auto-generated Swagger UI — full endpoint documentation with live request/response testing.

## PM Translation

| Code concept | PM equivalent |
|---|---|
| DTO | Stakeholder-facing report — you control what they see |
| `IRiskService` interface | API contract defined before implementation starts |
| PATCH /status only | Controlled workflow gate — not a free-form edit |
| `404 Not Found` vs `204 No Content` | Structured resolution states, not just "it worked" |
| Swagger UI | Living documentation, auto-generated from the code itself |

## Series Context

This is part of a short build series focused on architectural clarity over UI polish:

1. **Program Change Tracker** — Razor Pages, service-layer separation, enum-driven workflow, PRG pattern, soft delete
2. **Program Risk REST API** ← you are here
