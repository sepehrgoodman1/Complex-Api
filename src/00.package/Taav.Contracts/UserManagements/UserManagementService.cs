using Taav.Contracts.UserManagements.Dto;

namespace Taav.Contracts.UserManagements;

public interface UserManagementService
{
    Task<string?> GetUserId(string username);
    Task<string> Create(CreateUserDto user);
    Task<bool> IsInRole(string userId, string roleName);
    Task SetRole(string userId, string roleName);
    Task DeleteRole(string userId, string roleName);
}