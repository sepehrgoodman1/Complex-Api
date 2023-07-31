
using Ef.Persistence.ComplexProject;
using Ef.Persistence.ComplexProject.Blocks;
using Entity.Entyties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Dtos.Block;
using System.Numerics;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly IBlockRepository _repository;

        public BlockController(IBlockRepository dbContext)
        {
            _repository = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Get_BlocksDto>>> GetBlockList()
        {
            if (!  _repository.BlocksExist())
            {
                return NotFound();
            }

            return await _repository.GetAllBlocks();
        }

        [HttpGet("GetBy/{id:int}")]
        public async Task<ActionResult<Get_One_BlockDto>> GetBlock(int id)
        {
            if (!_repository.BlocksExist())
            {
                return NotFound();
            }

            var block = _repository.GetBlocksById(id);

            if (block == null)
            {
                return NotFound("Block With this id does not Exist!");
            }

            return await block;
        }
        [HttpGet("GetBy/{name}")]
        public async Task<ActionResult<List<Get_BlocksDto>>> GetBlockist(string name)
        {
            var blocks = _repository.FindBlockByName(name);
            
            return await blocks;
        }

        [HttpPost]
        public async Task<ActionResult<Block>> PostBlock(Add_BlockDto dto)
        {
            var block = new Block
            {
                Name = dto.Name,
                NumberUnits = dto.NumberUnits,
                ComplexId = dto.ComplexId,
            };

            if(! await _repository.ComplexIdExist(block.ComplexId))
            {
                return NotFound("ComplexId Not Found!");
            }

            if ( await _repository.CheckBlockName(block.Id, block.Name))
            {
                return BadRequest("Block Name Already Exist In Complex");
            }

            if (block.NumberUnits < 1)
            {
                return BadRequest("Number of Units Must be More than one unit");
            }

            _repository.AddBlock(block);

            return CreatedAtAction(nameof(GetBlockList), new { id = block.Id }, block);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateBlock(int id, Update_BlockDto BlockDto)
        {

            var block = await _repository.FindBlock(id);

            if (block == null)
            {
                return NotFound("Block with this id does not exist!");
            }

            if (BlockDto.NumberUnits > 0)
            {
                block.NumberUnits = BlockDto.NumberUnits;
                block.Name = BlockDto.Name;
            }
            else
            {
                return BadRequest("Number of Units Must be Greater than one");
            }
           
            _repository.SetEntry(block);

            /*var thisBlock = await _repository.Block.Include(b => b.Units).FirstOrDefaultAsync(x => x.Id == block.Id);*/

            if (block.Units.Any())
            {
                return BadRequest("For this block unit registered before! you cant change number of units.");

            }



           /* var listUnits = await _repository.Unit.ToListAsync();

            int CounterUnits = 0;

            foreach (var unit in listUnits)
            {
                if (block.Id == unit.BlockId)
                {
                    CounterUnits++;
                }
            }*/
            if (await _repository.CheckUnits(block.Id))
            {
                return BadRequest("For this block unit registered before! you cant change number of units.");
            }

            _repository.SaveBlock();

            
         
            return Ok();
        }
   




    }
}
