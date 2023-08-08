
using ComplexProject.Entities.Entyties;
using ComplexProject.Services.Blocks.Contracts.Dtos;
using Taav.Contracts.Interfaces;

namespace ComplexProject.Services.Blocks.Contracts
{
    public interface BlockRepository
    {
        Task<IPageResult<GetBlocksDto>> GetAll(IPagination? pagination);
        Task<GetOneBlockDto> GetById(int id);
        Task<List<GetBlocksDto>> FindByName(string name);
        void Add(Block block);
        Task<Block> FindBlock(int BlockId);
        Task Update(Block block);
        Task<bool> BlockIdDoesExist(int blockId);
        Task<bool> ExistUnitNameInBlock(int blockId, string tenant);
        Task<bool> IsExistByNameAndComplexId(int complexId, string name);
    }
}
