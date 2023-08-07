using Entity.Entyties;
using Services.Units.Contracts.Dtos;

namespace Services.Units.Contracts
{
    public interface UnitRepository
    {
        Task<bool> BlockIdDoesExist(int blockId);
        Task<List<GetUnitsDto>> GetAllUnits();

        Unit SetUnit(AddUnitDto dto);

        Task<bool> ExistUnitNameInBlock(int blockId, string tenant);
        Task<int> Add(Unit unit);
    }
}
