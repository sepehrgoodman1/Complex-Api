using Services.Units.Contracts.Dtos.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Units.Contracts
{
    public interface UnitService
    {
        Task<List<Get_UnitsDto>> GetAll();
        void Add(Add_UnitDto dto);
    }
}
