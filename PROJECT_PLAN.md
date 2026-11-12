# Project Plan — Preparing AppointmentSystem for the CV

Current state: the Domain and Application layers are solid (Clean Architecture, full CQRS with MediatR, Result pattern, domain events, MediatR behaviors for caching/logging/validation/exceptions). The API layer is still a stub (`Program.cs` only returns "Hello World" — nothing is wired up), and there are no tests, migrations, docs, or CI.

Work through the phases in order. Phases 1–2 are the priority: without them the project doesn't actually run.

## Phase 1 — Make the API work
- [ ] DI registration: `AddApplication()` (MediatR + behaviors + FluentValidation) and `AddInfrastructure()` (DbContext, Identity, token provider)
- [ ] Endpoints (Minimal API groups or Controllers) wired to the existing Commands/Queries for: Appointments, Categories, Services, TimeSlots, Identity
- [ ] Exception → `ProblemDetails` middleware built on the existing `Result`/error types
- [ ] JWT authentication/authorization wired to `TokenProvider` / `IdentityService`
- [ ] Swagger / OpenAPI

## Phase 2 — Database
- [ ] EF Core migrations, `AppDbContext` wired up in `Program.cs`
- [ ] Seed data (sample Categories/Services) for an easy demo

## Phase 3 — Tests
- [ ] Unit tests for the core handlers (xUnit + FluentAssertions + NSubstitute)
- [ ] At least one integration test (WebApplicationFactory) for the Book Appointment flow

## Phase 4 — Easy to run / demo
- [ ] Dockerfile + docker-compose (API + database), one-command run
- [ ] Optional: deploy a live demo (Render / Railway / Azure free tier)

## Phase 5 — Documentation & CI
- [ ] README: project description, architecture overview, setup instructions, screenshots/GIF of Swagger
- [ ] Simple GitHub Actions CI (build + test)

## Phase 6 — Optional polish
- [ ] Serilog structured logging
- [ ] Health check endpoint
- [ ] Basic rate limiting

---
*Also fixed: a file path with a trailing space (`Categories/Commands /...`) broke `git clone` on Windows — fixed directly on GitHub via the Git Data API.*
