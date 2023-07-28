using Apis.Dtos;
using ComplexApi.Dtos;
using ComplexApi.ComplexApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComplexApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly EFDataContext _dbContext;

        public UnitController(EFDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Get_UnitsDto>>> GetUnitList()
        {
            if (_dbContext.Unit == null)
            {
                return NotFound();
            }

            var unit = from b in _dbContext.Unit
                          select new Get_UnitsDto()
                          {
                              Tenant = b.Tenant,
                              TypeHouse= b.TypeHouse,
                          };

            return await unit.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(AddUnitDto dto)
        {

            var unit = new Unit
            {
                Tenant = dto.Tenant,
                TypeHouse = dto.TypeHouse.ToUpper(),
                BlockId = dto.BlockId,
            };


          
            if(!Enum.IsDefined(typeof(TypeOfUnits), unit.TypeHouse))
            {
                return BadRequest("Unit Type Must Be Only 'Owner', 'Tenant' or 'Anonymous'");
            }

            if (!_dbContext.Block.Any(x=> x.Id == unit.BlockId))
            {
                return BadRequest("Block Id Does Not Exist");
            }



            var listUnits = await _dbContext.Unit.Select(b => new { b.Tenant, b.BlockId }).ToListAsync();

            foreach (var single_unit in listUnits)
            {
                if (single_unit.BlockId == unit.BlockId && single_unit.Tenant == unit.Tenant)
                {
                    return BadRequest("Unit Name Already Exist In Complex");
                }
            }



            _dbContext.Unit.Add(unit);

            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUnitList), new { id = unit.Id }, unit);
        }

      
    




    }
}
enum TypeOfUnits
{
    OWNER,
    TENANT,
    ANONYMOUS
}
