using ComplexProject.Services.Units.Contracts.Dtos;

namespace ComplexProject.Services.Units.Contracts
{
    public interface UnitService
    {
        Task<List<GetUnitsDto>> GetAll();
        Task<int> Add(AddUnitDto dto);
    }
}
