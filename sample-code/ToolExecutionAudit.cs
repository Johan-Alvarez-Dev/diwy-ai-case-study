namespace Diwy.PublicSample;

public enum ToolExecutionOutcome { Succeeded, Rejected, Failed, Cancelled }

public sealed record ToolExecutionAudit(
    Guid ExecutionId,
    string ToolName,
    AuthorizationDecision Authorization,
    ToolExecutionOutcome Outcome,
    DateTimeOffset StartedAt,
    DateTimeOffset CompletedAt,
    string? FailureCode)
{
    public TimeSpan Duration => CompletedAt - StartedAt;

    public static ToolExecutionAudit Create(
        string toolName,
        AuthorizationDecision authorization,
        ToolExecutionOutcome outcome,
        DateTimeOffset startedAt,
        DateTimeOffset completedAt,
        string? failureCode = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(toolName);
        ArgumentOutOfRangeException.ThrowIfLessThan(completedAt, startedAt);
        if (outcome is ToolExecutionOutcome.Failed && string.IsNullOrWhiteSpace(failureCode))
            throw new ArgumentException("Failed executions require a stable failure code.", nameof(failureCode));
        if (outcome is not ToolExecutionOutcome.Failed && failureCode is not null)
            throw new ArgumentException("Only failed executions may contain a failure code.", nameof(failureCode));

        return new ToolExecutionAudit(
            Guid.NewGuid(), toolName.Trim(), authorization, outcome, startedAt, completedAt, failureCode);
    }
}
