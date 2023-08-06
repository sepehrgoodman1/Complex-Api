using Entity.Entyties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Blocks.Contracts;
using Services.Blocks.Contracts.Dtos;
using Services.Blocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Blocks
{


    public class BlockAppService : BlockService  
    {
        private readonly BlockRepository _repository;

        public BlockAppService(BlockRepository repository)
        {
            _repository = repository;
        }
       
    

        public async Task<IEnumerable<Get_BlocksDto>> GetAll()
        {
            if (! _repository.BlocksExist())
            {
                throw new NotFoundException();
            }

            return await _repository.GetAll();
        }
        public async Task<Get_One_BlockDto> GetById(int id)
        {
            if (!_repository.BlocksExist())
            {
                throw new NotFoundException();
            }

            var block = _repository.GetById(id);

            if (block == null)
            {
                throw new InvalidBlockIdException();
                /*return NotFound("Block With this id does not Exist!");*/
            }
            return await block;
        }

        public async Task<List<Get_BlocksDto>> GetByName(string name)
        {
            var block = await _repository.FindByName(name);
            return  block;
        }

        public async void Add(Add_BlockDto dto)
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

            if (await _repository.CheckBlockName(block.Id, block.Name))
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

            /*_repository.Save*/

        }

        public async void Update(int id, Update_BlockDto BlockDto)
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
            _repository.Update(block);

            if (await _repository.CheckUnits(block.Id))
            {
                throw new RegisteredUnitException();

                /*                return BadRequest("For this block unit registered before! you cant change number of units.");
                */
            }

            _repository.SaveBlock();
        }
    }
}
