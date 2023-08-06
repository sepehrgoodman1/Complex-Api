using Services.Units.Contracts.Dtos;

namespace Services.Units.Contracts
{
    public interface UnitService
    {
        Task<List<GetUnitsDto>> GetAll();
        Task<int> Add(AddUnitDto dto);
    }
}
