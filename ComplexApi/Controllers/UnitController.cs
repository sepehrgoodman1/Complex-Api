using Apis.Dtos;
using Ef.Persistence.ComplexProject.Units;
using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitRepository _repository;

        public UnitController(IUnitRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Get_UnitsDto>>> GetUnitList()
        {
            if (_repository.DataIsEmpty())
            {
                return NotFound();
            }

            var unit = 

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

            if (!await _repository.BlockIdDoesExist(unit.BlockId))
            {
                return BadRequest("Block Id Does Not Exist");
            }


            var block = await _repository.Block.Include(b => b.Units).FirstOrDefaultAsync(x => x.Id == unit.BlockId);

            if (block.Units.Select(b => b.Tenant).Contains(unit.Tenant))
            {
                return BadRequest("Unit Name Already Exist In Block");
            }

            _repository.Unit.Add(unit);

            await _repository.SaveChangesAsync();

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
