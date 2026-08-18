using Diwy.PublicSample;
using Xunit;

namespace Diwy.PublicSample.Tests;

public sealed class ToolExecutionAuditTests
{
    private static readonly DateTimeOffset Start = new(2026, 8, 18, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Captures_a_deterministic_duration()
    {
        var audit = ToolExecutionAudit.Create(
            "workspace.read", AuthorizationDecision.Allow, ToolExecutionOutcome.Succeeded, Start, Start.AddMilliseconds(250));

        Assert.Equal(TimeSpan.FromMilliseconds(250), audit.Duration);
        Assert.Equal("workspace.read", audit.ToolName);
    }

    [Fact]
    public void Failed_execution_requires_a_stable_code()
    {
        Assert.Throws<ArgumentException>(() => ToolExecutionAudit.Create(
            "workspace.write", AuthorizationDecision.Allow, ToolExecutionOutcome.Failed, Start, Start.AddSeconds(1)));
    }

    [Fact]
    public void Rejects_negative_duration()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => ToolExecutionAudit.Create(
            "workspace.read", AuthorizationDecision.Allow, ToolExecutionOutcome.Succeeded, Start, Start.AddSeconds(-1)));
    }
}
