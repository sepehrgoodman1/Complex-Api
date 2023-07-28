using Apis.Dtos;
using ComplexApi.Dtos;
using ComplexApi.ComplexApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Security.Cryptography.Xml;

namespace ComplexApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexController : ControllerBase
    {
        private readonly EFDataContext _dbContext;

        public ComplexController(EFDataContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("Complexes-With-RegistedUnit")]
        public async Task<ActionResult<IEnumerable<Get_ComplexDto>>> GetComplexList()
        {
            if(_dbContext.Complex == null)
            {
                return NotFound();
            }


            var complex = from c in _dbContext.Complex
                          select new Get_ComplexDto()
                          {
                              Id = c.Id,
                              Name = c.Name,
                              RegisteredUnits = c.Blocks.SelectMany(u=>u.Units).Count(),
                              NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count()
                          };


            return await complex.ToListAsync();
        }

        [HttpGet("Complexes-Detail-Blocks")]
        public async Task<ActionResult<IEnumerable<Get_Coplexes_Detail_BlocksDto>>> GetComplexWithDetailBlocks()
        {
            if (_dbContext.Complex == null)
            {
                return NotFound();
            }


            var complex = from c in _dbContext.Complex
                          select new Get_Coplexes_Detail_BlocksDto()
                          {
                              Name = c.Name,
                              BlockDetails = c.Blocks.Select( block => new { block.Name, block.NumberUnits })
                          };


            return await complex.ToListAsync();
        }




        [HttpGet("Complex-And-Number-Blocks/{id:int}")]
        public async Task<ActionResult<Get_Complex_And_CountBlock>> GetComplexAndBlock(int id)
        {
            if (_dbContext.Complex == null)
            {
                return NotFound();
            }


            var complex = await _dbContext.Complex.Select(c =>
                                                   new Get_Complex_And_CountBlock()
                                                   {
                                                       Id = c.Id,
                                                       Name = c.Name,
                                                       RegisteredUnits = c.Blocks.SelectMany(u => u.Units).Count(),
                                                       NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count(),
                                                       NumberOfBlocks = c.Blocks.Count()
                                                   }).SingleOrDefaultAsync(b => b.Id == id);

            return complex;
        }





        [HttpGet("GetBy/{id:int}")]
        public async Task<ActionResult<Get_ComplexDto>> GetComplexList(int id)
        {
            if (_dbContext.Complex == null)
            {
                return NotFound();
            }
       

            var complex = await _dbContext.Complex.Select(c =>
                                                   new Get_ComplexDto()
                                                   {
                                                       Id = c.Id,
                                                       Name = c.Name,
                                                       RegisteredUnits = c.Blocks.SelectMany(u => u.Units).Count(),
                                                       NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count()
                                                   }).SingleOrDefaultAsync(b => b.Id == id);
            
            if(complex == null)
            {
                return NotFound();
            }


            return complex;
        }



        [HttpGet("GetBy/{name}")]
        public async Task<ActionResult<IEnumerable<Get_ComplexDto>>> GetComplexList( string name)
        {
            IQueryable<Complex> query = _dbContext.Complex;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var complex = from c in query
                          select new Get_ComplexDto()
                          {
                              Id = c.Id,
                              Name = c.Name,
                              RegisteredUnits = c.Blocks.SelectMany(u => u.Units).Count(),
                              NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count()
                          };


            return await complex.ToListAsync();
        }



        [HttpPost]
        public async Task<ActionResult<Complex>> PostComplex(AddComplexDto dto)
        {
            var complex = new Complex
            {
                Name = dto.Name,
                NumberUnits = dto.NumberUnits
            };

            if (complex.NumberUnits < 4 || complex.NumberUnits > 1000)
            {
                return BadRequest("Number of Units Must be between 4 and 1000");
            }
            
            _dbContext.Complex.Add(complex);

            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComplexList), new {id = complex.Id}, complex);
        }


        [HttpPatch]
        public async Task<IActionResult> PutBrand( int id , UpdateComplexDto complexDto)
        {
            var complex = await _dbContext.Complex.FindAsync(id);
            if (complex == null)
            {
                return NotFound();
            }

            if (complexDto.NumberUnits != complex.NumberUnits)
            {
                // code here
            }
    
            complex.NumberUnits = complexDto.NumberUnits;
            _dbContext.Entry(complex).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (ComplexAvailable(id))
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
        private bool ComplexAvailable(int id)
        {
            return (_dbContext.Complex?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplex(int id)
        {
            if(_dbContext.Complex == null)
            {
                return NotFound();
            }
            var complex = await _dbContext.Complex.FindAsync(id);
            if (complex == null)
            {
                return NotFound();
            }
            _dbContext.Complex.Remove(complex);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

   


    }
}
