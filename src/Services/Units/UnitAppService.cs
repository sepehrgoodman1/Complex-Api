using Services.Units.Contracts;
using Services.Units.Contracts.Dtos;
using Services.Units.Exceptions;
using Taav.Contracts.Interfaces;

namespace Services.Units
{
    public class UnitAppService :UnitService
    {
        private readonly UnitRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public UnitAppService(UnitRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<GetUnitsDto>> GetAll()
        {

            return await _repository.GetAllUnits();
        }
        public async Task<int> Add(AddUnitDto dto)
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

            await _unitOfWork.Complete();

            return unit.Id;
        }
    }
}
enum TypeOfUnits
{
    OWNER,
    TENANT,
    ANONYMOUS
}

