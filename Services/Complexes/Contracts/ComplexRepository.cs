using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Services.Complexes.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Complexes.Contracts
{
    public interface ComplexRepository
    {
        Task<bool> IsNull();
        Task<List<Get_ComplexDto>> GetWithNumRegisteredUnits();
        Task<List<Get_Coplexes_Detail_BlocksDto>> GetComplexDetailBlock();
        Task<Get_Complex_And_CountBlock> GetComplexCountBlocks(int id);
        Task<Get_ComplexDto> GetAllWithRegUnit(int id);
        Task<List<Get_ComplexDto>> FindByName(string name);
        void Add(Complex complex);
        Task<Complex> GetComplexWithAllUnits(int id);
        void Update(Complex complex);
        Task<Complex> GetById(int id);
        void Remove(Complex complex);
    }
}
