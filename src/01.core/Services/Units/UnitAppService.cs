using ComplexProject.Entities.Entyties;
using ComplexProject.Services.Blocks.Contracts;
using ComplexProject.Services.Complexes.Contracts;
using ComplexProject.Services.Units.Contracts;
using ComplexProject.Services.Units.Contracts.Dtos;
using ComplexProject.Services.Units.Exceptions;
using Taav.Contracts.Interfaces;

namespace ComplexProject.Services.Units
{
    public class UnitAppService :UnitService
    {
        private readonly UnitRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly BlockRepository _blockRepository;

        public UnitAppService(
            UnitRepository repository,
            UnitOfWork unitOfWork,
            BlockRepository blockRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _blockRepository = blockRepository;
        }
        public async Task<List<GetUnitsDto>> GetAll()
        {

            return await _repository.GetAllUnits();
        }
        public async Task<int> Add(AddUnitDto dto)
        {
            var unit = new Unit
            {
                Tenant = dto.Tenant,
                TypeHouse = dto.TypeHouse.ToUpper(),
                BlockId = dto.BlockId,
            };

            if (!Enum.IsDefined(typeof(TypeOfUnits), unit.TypeHouse))
            {
                throw new UnitTypeExeption();
            }

            if (!await _blockRepository.BlockIdDoesExist(unit.BlockId))
            {
                throw new InvalidBlockIdException();
            }

            if (await _blockRepository.ExistUnitNameInBlock(unit.BlockId, unit.Tenant))
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

