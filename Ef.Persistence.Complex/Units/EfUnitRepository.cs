using Entity.Entyties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef.Persistence.ComplexProject.Units
{
    public class EfUnitRepository :IUnitRepository
    {
        private readonly EFDataContext _context;

        public async Task<bool> BlockIdDoesExist(int blockId)
        {
            return await _context.Block.AnyAsync(x => x.Id == blockId);
        }

        public bool DataIsEmpty()
        {
            if(_context.Unit == null)
            {
                return true;
            }
            else { return false; }
        }

        public Task<object> GetAllUnits()
        {
            return from b in _context.Unit
                   select new Get_UnitsDto()
                   {
                       Tenant = b.Tenant,
                       TypeHouse = b.TypeHouse,
                   };
        }
    }
}
