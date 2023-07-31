using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef.Persistence.ComplexProject.Blocks
{
    public interface IBlockRepository
    {
        bool BlocksExist();
        Task<List<Get_BlocksDto>> GetAllBlocks();
        Task<Get_One_BlockDto> GetBlocksById(int id);
        Task<ActionResult<List<Get_BlocksDto>>> FindBlockByName(string name);
        Task<bool> ComplexIdExist(int BlockId);
        Task<bool> CheckBlockName(int complexId, string blockName);
        void AddBlock(Block block);
        Task<Block> FindBlock(int BlockId);
        void SetEntry(Block block);
        Task<bool> CheckUnits(int BlockId);
        void SaveBlock();
    }
}
