﻿using Ef.Persistence.ComplexProject.Units;
using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Dtos.Unit;

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

            return await _repository.GetAllUnits();
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(Add_UnitDto dto)
        {
            var unit = _repository.SetUnit(dto);
          
            if(!Enum.IsDefined(typeof(TypeOfUnits), unit.TypeHouse))
            {
                return BadRequest("Unit Type Must Be Only 'Owner', 'Tenant' or 'Anonymous'");
            }

            if (!await _repository.BlockIdDoesExist(unit.BlockId))
            {
                return BadRequest("Block Id Does Not Exist");
            }

            if (await _repository.ExistUnitNameInBlock(unit.BlockId, unit.Tenant))
            {
                return BadRequest("Unit Name Already Exist In Block");
            }

            _repository.AddUnit(unit);

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
