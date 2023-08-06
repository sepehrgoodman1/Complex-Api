using Entity.Entyties;
using Services.Units.Contracts;
using Services.Units.Contracts.Dtos.Unit;
using Services.Units.Exceptions;
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
                throw new NotFoundException();
            }

            return await _repository.GetAllUnits();
        }
        public async void Add(Add_UnitDto dto)
        {
            var unit = _repository.SetUnit(dto);

            if (!Enum.IsDefined(typeof(TypeOfUnits), unit.TypeHouse))
            {
                throw new UnitTypeExeption();
          /*      return BadRequest("Unit Type Must Be Only 'Owner', 'Tenant' or 'Anonymous'");*/
            }

            if (!await _repository.BlockIdDoesExist(unit.BlockId))
            {
                throw new InvalidBlockIdException();
            }

            if (await _repository.ExistUnitNameInBlock(unit.BlockId, unit.Tenant))
            {
                throw new DuplicateUnitNameException();
            }

            _repository.Add(unit);
        }
    }
}
enum TypeOfUnits
{
    OWNER,
    TENANT,
    ANONYMOUS
}

