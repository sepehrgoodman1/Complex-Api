using Apis.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Security.Cryptography.Xml;
using Ef.Persistence.ComplexProject.Complexes;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexController : ControllerBase
    {
        private readonly IComplexRepository _repository;

        public ComplexController(IComplexRepository dbContext)
        {
            _repository = dbContext;
        }


        [HttpGet("Complexes-With-RegistedUnit")]
        public async Task<ActionResult<IEnumerable<Get_ComplexDto>>> GetComplexList()
        {
            if(_repository.Complex == null)
            {
                return NotFound();
            }


            var complex = from c in _repository.Complex
                          select new Get_ComplexDto()
                          {
                              Id = c.Id,
                              Name = c.Name,
                              RegisteredUnits = c.Blocks.SelectMany(u=>u.Units).Count(),
                              NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count()
                          };


            if (!complex.Any())
            {
                return NotFound();
            }

            return await complex.ToListAsync();
        }

        [HttpGet("Complexes-Detail-Blocks")]
        public async Task<ActionResult<IEnumerable<Get_Coplexes_Detail_BlocksDto>>> GetComplexWithDetailBlocks()
        {
            if (_repository.Complex == null)
            {
                return NotFound();
            }


            var complex = from c in _repository.Complex
                          select new Get_Coplexes_Detail_BlocksDto()
                          {
                              Name = c.Name,
                              BlockDetails = c.Blocks.Select( block => new { block.Name, block.NumberUnits })
                          };

            if (!complex.Any())
            {
                return NotFound();
            }


            return await complex.ToListAsync();
        }




        [HttpGet("Complex-And-Number-Blocks/{id:int}")]
        public async Task<ActionResult<Get_Complex_And_CountBlock>> GetComplexAndBlock(int id)
        {
            if (_repository.Complex == null)
            {
                return NotFound();
            }


            var complex = await _repository.Complex.Select(c =>
                                                   new Get_Complex_And_CountBlock()
                                                   {
                                                       Id = c.Id,
                                                       Name = c.Name,
                                                       RegisteredUnits = c.Blocks.SelectMany(u => u.Units).Count(),
                                                       NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count(),
                                                       NumberOfBlocks = c.Blocks.Count()
                                                   }).SingleOrDefaultAsync(b => b.Id == id);

            if (complex == null)
            {
                return NotFound();
            }

            return complex;
        }

        [HttpGet("GetBy/{id:int}")]
        public async Task<ActionResult<Get_ComplexDto>> GetComplexList(int id)
        {
            if (_repository.Complex == null)
            {
                return NotFound();
            }
       

            var complex = await _repository.Complex.Select(c =>
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
            IQueryable<Complex> query = _repository.Complex;

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


            if (!complex.Any())
            {
                return NotFound();
            }

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
            
            _repository.Complex.Add(complex);

            await _repository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComplexList), new {id = complex.Id}, complex);
        }


        [HttpPatch]
        public async Task<IActionResult> PutComplex( int id , UpdateComplexDto complexDto)
        {

            var complex = await _repository.Complex.Include(c => c.Blocks).ThenInclude(c => c.Units).FirstAsync(x=>x.Id == id);

            if (complex == null)
            {
                return NotFound();
            }
            
            if (complex.Blocks.SelectMany(x => x.Units).Any())
            {
                return BadRequest("for this complex there is registered unit, you cant change number of units!");
            }


            complex.NumberUnits = complexDto.NumberUnits;
            
            _repository.Entry(complex).State = EntityState.Modified;

            try
            {
                await _repository.SaveChangesAsync();
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
            return (_repository.Complex?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplex(int id)
        {
            if(_repository.Complex == null)
            {
                return NotFound();
            }
            var complex = await _repository.Complex.FindAsync(id);
            if (complex == null)
            {
                return NotFound();
            }
            _repository.Complex.Remove(complex);
            await _repository.SaveChangesAsync();

            return Ok();
        }

   


    }
}
