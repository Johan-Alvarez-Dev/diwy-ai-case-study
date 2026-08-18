namespace Diwy.PublicSample;

public enum ToolRisk { Low, Medium, High, Critical }
public enum AuthorizationDecision { Allow, Ask, Deny }

public sealed record ToolRequest(
    string ToolName,
    ToolRisk Risk,
    bool IsAuthenticated,
    bool HasExplicitConsent);

public sealed class ToolAuthorizationPolicy
{
    private readonly HashSet<string> _blockedTools;

    public ToolAuthorizationPolicy(IEnumerable<string>? blockedTools = null)
    {
        _blockedTools = new HashSet<string>(
            blockedTools ?? [],
            StringComparer.OrdinalIgnoreCase);
    }

    public AuthorizationDecision Evaluate(ToolRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var toolName = request.ToolName.Trim();
        if (toolName.Length == 0)
            throw new ArgumentException("Tool name is required.", nameof(request));

        if (!request.IsAuthenticated || _blockedTools.Contains(toolName))
            return AuthorizationDecision.Deny;

        return request.Risk switch
        {
            ToolRisk.Critical => AuthorizationDecision.Deny,
            ToolRisk.High when !request.HasExplicitConsent => AuthorizationDecision.Ask,
            ToolRisk.Medium when !request.HasExplicitConsent => AuthorizationDecision.Ask,
            _ => AuthorizationDecision.Allow
        };
    }
}
