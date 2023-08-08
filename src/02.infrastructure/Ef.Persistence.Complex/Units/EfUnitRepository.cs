using ComplexProject.Entities.Entyties;
using ComplexProject.Services.Units.Contracts;
using ComplexProject.Services.Units.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;


namespace ComplexProject.Persistence.Ef.Units
{
    public class EfUnitRepository :UnitRepository
    {
        private readonly DbSet<Unit> _units;

        public EfUnitRepository(
            EFDataContext context)
        {
            _units = context.Units;

        }
     

        public async Task<List<GetUnitsDto>> GetAllUnits()
        {
            var units =  from b in _units
                    select  new GetUnitsDto()
                    {
                        Tenant = b.Tenant,
                        TypeHouse = b.TypeHouse,
                    };

            return await units.ToListAsync();
        }

       
     
        public async Task<int> Add(Unit unit)
        {
            _units.Add(unit);

            return unit.Id;
        }
        public async Task<bool> HasAnyRegisteredUnits(int blockId)
        {
            return await _units.AnyAsync(_=>_.BlockId == blockId);
        }
    }
}
