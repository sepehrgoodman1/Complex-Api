using Services.Complexes.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Complexes.Contracts
{
    public interface ComplexService
    {
        Task<List<Get_ComplexDto>> GetAll_WithRegUnit();
        Task<List<Get_Coplexes_Detail_BlocksDto>> GetAll_WithBlockDetail();
        Task<Get_Complex_And_CountBlock> GetById_WithNumBlocks(int id);
        Task<Get_ComplexDto> GetById(int id);
        Task<List<Get_ComplexDto>> GetByName(string name);
        void Add(AddComplexDto dto);
        void Update(int id, UpdateComplexDto complexDto);
        void Delete(int id);
    }
}
