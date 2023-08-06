using Entity.Entyties;
using Services.Blocks.Contracts;
using Services.Blocks.Contracts.Dtos;
using Services.Blocks.Exceptions;
using Taav.Contracts.Interfaces;

namespace Services.Blocks
{
    public class BlockAppService : BlockService  
    {
        private readonly BlockRepository _repository;

        private readonly UnitOfWork _unitOfWork;

        public BlockAppService(BlockRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
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

        public async Task<List<GetBlocksDto>> GetByName(string name)
        {
            var block = await _repository.FindByName(name);
            return  block;
        }

        public async Task<int> Add(AddBlockDto dto)
        {
            var block = new Block
            {
                Name = dto.Name,
                NumberUnits = dto.NumberUnits,
                ComplexId = dto.ComplexId,
            };

            if (!await _repository.ComplexIdExist(block.ComplexId))
            {
                throw new InvalidComplexIdException();
                /*return NotFound("ComplexId Not Found!");*/
            }

            if (await _repository.CheckBlockName(block.ComplexId, block.Name))
            {
                throw new DuplicateBlockNameException();
               /* return BadRequest("Block Name Already Exist In Complex");*/
            }

            if (block.NumberUnits < 1)
            {
                throw new NumberUnitsException();
                /*return BadRequest("Number of Units Must be More than one unit");*/
            }

            _repository.Add(block);

            await _unitOfWork.Complete();

            return block.Id;

            /*_repository.Save*/

        }

        public async Task Update(int id, UpdateBlockDto BlockDto)
        {
            var block = await _repository.FindBlock(id);

            if (block == null)
            {
                throw new NotFoundException();
                /*return NotFound("Block with this id does not exist!");*/
            }

            if (BlockDto.NumberUnits > 0)
            {
                block.NumberUnits = BlockDto.NumberUnits;
                block.Name = BlockDto.Name;
            }
            else
            {
                throw new NumberUnitsException();
                /*return BadRequest("Number of Units Must be Greater than one");*/
            }

            if (await _repository.CheckUnits(block.Id))
            {
                throw new RegisteredUnitException();

                /*                return BadRequest("For this block unit registered before! you cant change number of units.");
                */
            }
            _repository.Update(block);

            await _unitOfWork.Complete();
        }
    }
}
