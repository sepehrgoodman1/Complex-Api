using ComplexProject.Services.Blocks.Contracts.Dtos;
using Taav.Contracts.Interfaces;

namespace ComplexProject.Services.Blocks.Contracts
{
    public interface BlockService
    {
        Task<GetOneBlockDto> GetById(int id);
        Task<IPageResult<GetBlocksDto>> GetAll(IPagination? pagination);
        Task<List<GetBlocksDto>> GetByName(string name);
        Task<int> Add(AddBlockDto dto);
        Task Update(int id, UpdateBlockDto BlockDto);
    }
}
