using Taav.Contracts.UserManagements.Dto;

namespace Taav.Contracts.UserManagements;

public interface TokenService
{
    public Task<Token> Get();
}