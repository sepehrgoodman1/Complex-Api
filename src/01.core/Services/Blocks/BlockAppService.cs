
using ComplexProject.Entities.Entyties;
using ComplexProject.Services.Blocks.Contracts;
using ComplexProject.Services.Blocks.Contracts.Dtos;
using ComplexProject.Services.Blocks.Exceptions;
using ComplexProject.Services.Complexes.Contracts;
using ComplexProject.Services.Units.Contracts;
using Taav.Contracts.Interfaces;

namespace ComplexProject.Services.Blocks
{
    public class BlockAppService : BlockService  
    {
        private readonly UnitOfWork _unitOfWork;

        private readonly BlockRepository _repository;
        private readonly ComplexRepository _complexRepository;
        private readonly UnitRepository _unitRepository;

        public BlockAppService(
            BlockRepository repository,
            UnitOfWork unitOfWork,
            ComplexRepository complexRepository,
            UnitRepository unitRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _complexRepository = complexRepository;
            _unitRepository = unitRepository;
        }

        public async Task<IPageResult<GetBlocksDto>> GetAll(IPagination? pagination)
        {
            return await _repository.GetAll( pagination);
        }
        public async Task<GetOneBlockDto> GetById(int id)
        {
          
            var block = _repository.GetById(id);

            if (block == null)
            {
                throw new InvalidBlockIdException();
                /*return NotFound("Block With this id does not Exist!");*/
            }
            return await block;
        }

        public async Task<List<GetBlocksDto>> GetByName( string name)
        {
            var block = await _repository.FindByName(name);
            return  block;
        }

        public async Task<int> Add(AddBlockDto dto)
        {
            if (!await _complexRepository.ComplexIdExist(dto.ComplexId))
            {
                throw new InvalidComplexIdException();
            }

            if (await _repository.IsExistByNameAndComplexId(dto.ComplexId, dto.Name))
            {
                throw new DuplicateBlockNameException();
            }

            var block = new Block
            {
                Name = dto.Name,
                NumberUnits = dto.NumberUnits,
                ComplexId = dto.ComplexId,
            };

            _repository.Add(block);

            await _unitOfWork.Complete();

            return block.Id;


        }

        public async Task Update(int id, UpdateBlockDto BlockDto)
        {
            var block = await _repository.FindBlock(id);

            if (block == null)
            {
                throw new NotFoundException();
            }

            if (BlockDto.NumberUnits > 0)
            {
                block.NumberUnits = BlockDto.NumberUnits;
                block.Name = BlockDto.Name;
            }
            else
            {
                throw new NumberUnitsException();
            }

            if (await _unitRepository.HasAnyRegisteredUnits(block.Id))
            {
                throw new RegisteredUnitException();

            }
            await _repository.Update(block);

            await _unitOfWork.Complete();
        }
    }
}
