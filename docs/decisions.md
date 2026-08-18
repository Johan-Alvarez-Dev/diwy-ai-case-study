# Technical Decisions

## One core, multiple clients

**Decision:** web/desktop use HTTP+SSE and CLI uses NDJSON, while behavior stays in .NET. **Why:** prevents three drifting agent implementations. **Trade-off:** transport contracts require deliberate versioning.

## Provider-neutral contracts

**Decision:** every provider maps into internal message, stream, tool, and generation types. **Why:** application code does not depend on one vendor SDK. **Trade-off:** vendor-only features need controlled extensions.

## Authorization before execution

**Decision:** a tool request resolves to `allow`, `ask`, or `deny`. **Why:** model output is not user permission. **Trade-off:** confirmation becomes an explicit application state.

## Reversible mutations

**Decision:** file changes are reviewable proposals with checkpoints. **Why:** failures become recoverable. **Trade-off:** additional persistence and reconciliation.

## Dual persistence

**Decision:** PostgreSQL serves hosted deployments while SQLite supports desktop/CLI. **Why:** local clients must operate without a hosted database. **Trade-off:** migrations and type mappings require provider-specific verification.
