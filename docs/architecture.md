# Public Architecture

## Core rule

All agent and business behavior lives in the .NET core. Clients transport commands and render state; they do not reimplement authorization, tool routing, or provider behavior.

## Dependency boundaries

| Layer | Owns | Must not depend on |
| --- | --- | --- |
| Domain | Entities, state transitions, invariants | ASP.NET Core, EF Core, providers |
| Application | Use cases, contracts, validation, orchestration | Concrete persistence or UI |
| Infrastructure | EF Core, encryption, provider adapters, filesystems, runners | Client presentation |
| API / CLI hosts | Authentication, transport, dependency composition | Duplicated business rules |
| Clients | Interaction, rendering, local UI state | Secrets or execution policy |

## Tool execution sequence

1. Authenticate the caller and resolve the active user/session.
2. Build a provider-neutral generation request.
3. Parse a proposed tool call into an internal contract.
4. Validate its schema and classify its risk.
5. Resolve `allow`, `ask`, or `deny` before execution.
6. Execute inside the appropriate workspace boundary.
7. Persist an audit-safe result and stream progress to the client.

## Quality attributes

- **Security:** explicit denial rules, human confirmation, encrypted secrets.
- **Portability:** provider adapters and dual PostgreSQL/SQLite persistence.
- **Recoverability:** cancellation, checkpoints, reviewable diffs.
- **Testability:** pure policies separated from transport and I/O.
- **Observability:** structured outcomes without logging secret content.
