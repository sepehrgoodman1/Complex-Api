using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Dtos.Unit;
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

     
        public async Task<List<Get_UnitsDto>> GetAllUnits()
        {
            var units =  from b in _context.Unit
                    select  new Get_UnitsDto()
                    {
                        Tenant = b.Tenant,
                        TypeHouse = b.TypeHouse,
                    };

            return await units.ToListAsync();
        }

        public Unit SetUnit(Add_UnitDto dto)
        {
            var unit = new Unit
            {
                Tenant = dto.Tenant,
                TypeHouse = dto.TypeHouse.ToUpper(),
                BlockId = dto.BlockId,
            };
            return unit;
        }

        public async Task<bool> ExistUnitNameInBlock(int blockId, string tenant)
        {
            var block = await _context.Block.Include(b => b.Units).FirstOrDefaultAsync(x => x.Id == blockId);

            return  block.Units.Select(b => b.Tenant).Contains(tenant);
        }


        public async void AddUnit(Unit unit)
        {
            _context.Unit.Add(unit);

            await _context.SaveChangesAsync();

        }
    }
}
