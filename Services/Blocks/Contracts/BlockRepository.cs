using Entity.Entyties;
using Services.Blocks.Contracts.Dtos;
using Taav.Contracts.Interfaces;

namespace Services.Blocks.Contracts
{
    public interface BlockRepository
    {
        Task<IPageResult<GetBlocksDto>> GetAll(IPagination? pagination);
        Task<GetOneBlockDto> GetById(int id);
        Task<List<GetBlocksDto>> FindByName(string name);
        Task<bool> ComplexIdExist(int BlockId);
        Task<bool> CheckBlockName(int complexId, string blockName);
        void Add(Block block);
        Task<Block> FindBlock(int BlockId);
        Task Update(Block block);
        Task<bool> CheckUnits(int BlockId);
    }
}
