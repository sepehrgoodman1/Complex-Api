using ComplexApi.Dtos;
using ComplexApi.ComplexApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComplexApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly EFDataContext _dbContext;

        public BlockController(EFDataContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Get_BlocksDto>>> GetBlockList()
        {
            if (_dbContext.Complex == null)
            {
                return NotFound();
            }

            var block = from b in _dbContext.Block
                          select new Get_BlocksDto()
                          {
                              Name = b.Name,
                              NumberUnits = b.NumberUnits,
                              RegisteredUnits = b.Units.Count(),
                              NotRegistedredUnits = b.NumberUnits - b.Units.Count()
                          };

            return await block.ToListAsync();
        }

        [HttpGet("GetBy/{id:int}")]
        public async Task<ActionResult<Get_One_BlockDto>> GetBlock(int id)
        {
            if (_dbContext.Block == null)
            {
                return NotFound();
            }

            var FindedBlock = await _dbContext.Block.FindAsync(id);

            Get_One_BlockDto block = new Get_One_BlockDto();

            block.Name = FindedBlock.Name;
            block.Units = FindedBlock.Units;

            if (block == null)
            {
                return NotFound();
            }
            return block;
        }
        [HttpGet("GetBy/{name}")]
        public async Task<ActionResult<IEnumerable<Get_BlocksDto>>> GetBlockist(string name)
        {
            IQueryable<Block> query = _dbContext.Block;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var complex = from b in query
                          select new Get_BlocksDto()
                          {
                              Name = b.Name,
                              NumberUnits = b.NumberUnits,
                              RegisteredUnits = b.Units.Count(),
                              NotRegistedredUnits = b.NumberUnits - b.Units.Count()
                          };


            return await complex.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Block>> PostBlock(AddBlockDto dto)
        {

            var block = new Block
            {
                Name = dto.Name,
                NumberUnits = dto.NumberUnits,
                ComplexId = dto.ComplexId,
            };


            if (block.NumberUnits < 1)
            {
                return BadRequest("Number of Units Must be More than one unit");
            }
            if (_dbContext.Block.Any(b=>b.Name == block.Name) && _dbContext.Block.Any(b => b.ComplexId == block.ComplexId))
            {
                return BadRequest("Block Name Already Exist In Complex");
            }



            _dbContext.Block.Add(block);

            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlockList), new { id = block.Id }, block);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlock(int id, UpdateBlockDto BlockDto)
        {

            var block = await _dbContext.Block.FindAsync(id);

            if (block == null)
            {
                return NotFound();
            }

            if (block.Units != null)
            {
                return BadRequest("At Least One Unit Registered! You Cant Change Number of units");
            }
            if (BlockDto.NumberUnits > 0)
            {
                block.NumberUnits = BlockDto.NumberUnits;
            }
            else
            {
                return BadRequest("Number of Units Must be More than one unit");
            }

            _dbContext.Entry(block).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!blockAvailable(block.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return Ok();
        }
        private bool blockAvailable(int id)
        {
            return (_dbContext.Block?.Any(x => x.Id == id)).GetValueOrDefault();
        }




    }
}
