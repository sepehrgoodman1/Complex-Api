using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.Block;
using Services.Dtos.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef.Persistence.ComplexProject.Complexes
{
    public interface IComplexRepository
    {
        Task<bool> complexIsNull();
        Task<List<Get_ComplexDto>> GetComplexRegUnit();
        Task<List<Get_Coplexes_Detail_BlocksDto>> GetComplexDetailBlock();
        Task<Get_Complex_And_CountBlock> GetComplexCountBlocks(int id);
        Task<Get_ComplexDto> GetComplexById(int id);
        Task<List<Get_ComplexDto>> FindComplexByName(string name);
        void AddComplex(Complex complex);
        Task<Complex> GetAllComplexWithUnits(int id);
        void SetEntry(Complex complex);
        Task<Complex> FindComplexById(int id);
        void RemoveComplex(Complex complex);
    }
}
