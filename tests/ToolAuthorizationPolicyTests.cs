using Diwy.PublicSample;
using Xunit;

namespace Diwy.PublicSample.Tests;

public sealed class ToolAuthorizationPolicyTests
{
    private readonly ToolAuthorizationPolicy _policy = new(["shell.root"]);

    [Fact]
    public void Denies_unauthenticated_requests()
    {
        var request = new ToolRequest("workspace.read", ToolRisk.Low, false, false);
        Assert.Equal(AuthorizationDecision.Deny, _policy.Evaluate(request));
    }

    [Fact]
    public void Asks_before_high_risk_tool_without_consent()
    {
        var request = new ToolRequest("workspace.write", ToolRisk.High, true, false);
        Assert.Equal(AuthorizationDecision.Ask, _policy.Evaluate(request));
    }

    [Fact]
    public void Allows_high_risk_tool_after_explicit_consent()
    {
        var request = new ToolRequest("workspace.write", ToolRisk.High, true, true);
        Assert.Equal(AuthorizationDecision.Allow, _policy.Evaluate(request));
    }

    [Theory]
    [InlineData("shell.root")]
    [InlineData("SHELL.ROOT")]
    public void Denies_blocked_tools_case_insensitively(string toolName)
    {
        var request = new ToolRequest(toolName, ToolRisk.Low, true, true);
        Assert.Equal(AuthorizationDecision.Deny, _policy.Evaluate(request));
    }
}
