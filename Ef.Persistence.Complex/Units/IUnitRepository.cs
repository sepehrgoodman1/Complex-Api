using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef.Persistence.ComplexProject.Units
{
    public interface IUnitRepository
    {
        Task<bool> BlockIdDoesExist(int blockId);
        bool DataIsEmpty();
        Task<List<Get_UnitsDto>> GetAllUnits();

        Unit SetUnit(Add_UnitDto dto);

        Task<bool> ExistUnitNameInBlock(int blockId, string tenant);
        void AddUnit(Unit unit);
    }
}
