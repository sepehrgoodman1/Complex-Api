using ComplexProject.Entities.Entyties;
using ComplexProject.Services.Units.Contracts.Dtos;


namespace ComplexProject.Services.Units.Contracts
{
    public interface UnitRepository
    {
        Task<List<GetUnitsDto>> GetAllUnits();


        Task<int> Add(Unit unit);
        Task<bool> HasAnyRegisteredUnits(int BlockId);

    }
}
