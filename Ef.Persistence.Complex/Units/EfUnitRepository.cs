using Entity.Entyties;
using Microsoft.EntityFrameworkCore;
using Services.Units.Contracts;
using Services.Units.Contracts.Dtos;

namespace Ef.Persistence.ComplexProject.Units
{
    public class EfUnitRepository :UnitRepository
    {
        private readonly EFDataContext _context;


        public EfUnitRepository(EFDataContext context)
        {
            _context = context;
        }
        public async Task<bool> BlockIdDoesExist(int blockId)
        {
            return await _context.Block.AnyAsync(x => x.Id == blockId);
        }



     
        public async Task<List<GetUnitsDto>> GetAllUnits()
        {
            var units =  from b in _context.Unit
                    select  new GetUnitsDto()
                    {
                        Tenant = b.Tenant,
                        TypeHouse = b.TypeHouse,
                    };

            return await units.ToListAsync();
        }

        public Unit SetUnit(AddUnitDto dto)
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


        public async Task<int> Add(Unit unit)
        {
            _context.Unit.Add(unit);

            return unit.Id;
        }
    }
}
