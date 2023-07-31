using Entity.Entyties;
using Services.Units.Contracts;
using Services.Units.Contracts.Dtos.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Units
{
    public class UnitAppService :UnitService
    {
        private readonly UnitRepository _repository;

        public UnitAppService(UnitRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Get_UnitsDto>> GetAll()
        {
            if (_repository.DataIsEmpty())
            {
               /* return NotFound();*/
            }

            return await _repository.GetAllUnits();
        }
        public async void Add(Add_UnitDto dto)
        {
            var unit = _repository.SetUnit(dto);

            if (!Enum.IsDefined(typeof(TypeOfUnits), unit.TypeHouse))
            {
          /*      return BadRequest("Unit Type Must Be Only 'Owner', 'Tenant' or 'Anonymous'");*/
            }

            if (!await _repository.BlockIdDoesExist(unit.BlockId))
            {
/*                return BadRequest("Block Id Does Not Exist");*/
            }

            if (await _repository.ExistUnitNameInBlock(unit.BlockId, unit.Tenant))
            {
              /*  return BadRequest("Unit Name Already Exist In Block");*/
            }

            _repository.AddUnit(unit);
        }
    }
}
enum TypeOfUnits
{
    OWNER,
    TENANT,
    ANONYMOUS
}

