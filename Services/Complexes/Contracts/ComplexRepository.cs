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
        Task<bool> complexIsNull();
        Task<List<Get_ComplexDto>> GetComplexRegUnit();
        Task<List<Get_Coplexes_Detail_BlocksDto>> GetComplexDetailBlock();
        Task<Get_Complex_And_CountBlock> GetComplexCountBlocks(int id);
        Task<Get_ComplexDto> GetComplexById(int id);
        Task<List<Get_ComplexDto>> FindComplexByName(string name);
        void AddComplex(Complex complex);
        Task<Complex> GetAllComplexWithUnits(int id);
        void Update(Complex complex);
        Task<Complex> FindComplexById(int id);
        void Remove(Complex complex);
    }
}
