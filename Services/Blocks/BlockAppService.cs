using Entity.Entyties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Blocks.Contracts;
using Services.Blocks.Contracts.Dtos;
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
              /*  return NotFound();*/
            }

            return await _repository.GetAllBlocks();
        }
        public async Task<Get_One_BlockDto> GetById(int id)
        {
            if (!_repository.BlocksExist())
            {
                /*return NotFound();*/
            }

            var block = _repository.GetBlocksById(id);

            if (block == null)
            {
                /*return NotFound("Block With this id does not Exist!");*/
            }
            return await block;
        }

        public async Task<List<Get_BlocksDto>> GetByName(string name)
        {
            var block = await _repository.FindBlockByName(name);
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
                /*return NotFound("ComplexId Not Found!");*/
            }

            if (await _repository.CheckBlockName(block.Id, block.Name))
            {
               /* return BadRequest("Block Name Already Exist In Complex");*/
            }

            if (block.NumberUnits < 1)
            {
                /*return BadRequest("Number of Units Must be More than one unit");*/
            }

            _repository.AddBlock(block);

            /*_repository.Save*/

            /*return CreatedAtAction(nameof(GetBlockList), new { id = block.Id }, block);*/
        }

        public async void Update(int id, Update_BlockDto BlockDto)
        {
            var block = await _repository.FindBlock(id);

            if (block == null)
            {
                /*return NotFound("Block with this id does not exist!");*/
            }

            if (BlockDto.NumberUnits > 0)
            {
                block.NumberUnits = BlockDto.NumberUnits;
                block.Name = BlockDto.Name;
            }
            else
            {
                /*return BadRequest("Number of Units Must be Greater than one");*/
            }
            _repository.Update(block);


            if (block.Units.Any())
            {
/*                return BadRequest("For this block unit registered before! you cant change number of units.");
*/
            }
         
            if (await _repository.CheckUnits(block.Id))
            {
/*                return BadRequest("For this block unit registered before! you cant change number of units.");
*/            }

            _repository.SaveBlock();
        }
    }
}
