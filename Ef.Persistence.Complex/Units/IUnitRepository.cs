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
        Task<object> GetAllUnits();
    }
}
