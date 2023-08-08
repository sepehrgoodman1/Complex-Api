
using ComplexProject.Services.Complexes.Contracts.Dtos;

namespace ComplexProject.Services.Complexes.Contracts
{
    public interface ComplexService
    {
        Task<List<GetComplexDto>> GetAll_WithRegUnit();
        Task<List<GetCoplexesDetailBlocksDto>> GetAll_WithBlockDetail();
        Task<GetComplexAndCountBlock> GetById_WithNumBlocks(int id);
        Task<GetComplexDto> GetById(int id);
        Task<List<GetComplexDto>> GetByName(string name);
        Task<int> Add(AddComplexDto dto);
        Task Update(int id, UpdateComplexDto complexDto);
    }
}
