using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Services.Blocks.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Blocks.Contracts
{
    public interface BlockRepository
    {
        bool BlocksExist();
        Task<List<Get_BlocksDto>> GetAll();
        Task<Get_One_BlockDto> GetById(int id);
        Task<List<Get_BlocksDto>> FindByName(string name);
        Task<bool> ComplexIdExist(int BlockId);
        Task<bool> CheckBlockName(int complexId, string blockName);
        void Add(Block block);
        Task<Block> FindBlock(int BlockId);
        void Update(Block block);
        Task<bool> CheckUnits(int BlockId);
        void SaveBlock();
    }
}
