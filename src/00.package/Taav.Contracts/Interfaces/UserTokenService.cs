namespace Taav.Contracts.Interfaces;

public interface UserTokenService
{
    string? UserId { get; }
    List<string> GetRoles();
    string? UserName { get; }
    string? TenantId { get; }
    bool IsInRole(string roleName);
}