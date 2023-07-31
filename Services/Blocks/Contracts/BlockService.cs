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
    public interface BlockService
    {
        Task<Get_One_BlockDto> GetById(int id);
        Task<IEnumerable<Get_BlocksDto>> GetAll();
        Task<List<Get_BlocksDto>> GetByName(string name);
        void Add(Add_BlockDto dto);
        void Update(int id, Update_BlockDto BlockDto);
    }
}
