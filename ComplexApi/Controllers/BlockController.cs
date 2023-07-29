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



            var block = await _dbContext.Block.Select(b =>
                                                  new Get_One_BlockDto()
                                                  {
                                                      Id = b.Id,
                                                      Name = b.Name,
                                                      UnitDetails = b.Units.Select(u=> new {u.Tenant, u.TypeHouse})
                                                  }).SingleOrDefaultAsync(c => c.Id == id);


            if (block == null)
            {
                return NotFound("Block With this id does not Exist!");
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

            if(!_dbContext.Complex.Any(x=>x.Id == block.ComplexId))
            {
                return NotFound("ComplexId Not Found!");
            }

            var complex = await _dbContext.Complex.Include(b => b.Blocks).FirstOrDefaultAsync(x => x.Id == block.ComplexId);

            if (complex.Blocks.Select(b => b.Name).Contains(block.Name))
            {
                return  BadRequest("Block Name Already Exist In Complex");
            }

            if (block.NumberUnits < 1)
            {
                return BadRequest("Number of Units Must be More than one unit");
            }

            _dbContext.Block.Add(block);

            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlockList), new { id = block.Id }, block);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateBlock(int id, UpdateBlockDto BlockDto)
        {

            var block = await _dbContext.Block.FindAsync(id);

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

            _dbContext.Entry(block).State = EntityState.Modified;

            var thisBlock = await _dbContext.Block.Include(b => b.Units).FirstOrDefaultAsync(x => x.Id == block.Id);

            if (thisBlock.Units.Any())
            {
                return BadRequest("For this block unit registered before! you cant change number of units.");

            }

            var listUnits = await _dbContext.Unit.ToListAsync();

            int CounterUnits = 0;

            foreach (var unit in listUnits)
            {
                if (block.Id == unit.BlockId)
                {
                    CounterUnits++;
                }
            }
            if (CounterUnits > 0)
            {
                return BadRequest("For this block unit registered before! you cant change number of units.");
            }

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
