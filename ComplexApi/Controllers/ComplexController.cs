using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Security.Cryptography.Xml;
using Ef.Persistence.ComplexProject.Complexes;
using Services.Dtos.Complex;
using Entity.Entyties;

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
            if(await _repository.complexIsNull())
            {
                return NotFound();
            }


            var complex = await _repository.GetComplexRegUnit();


            if (!complex.Any())
            {
                return NotFound();
            }

            return  complex;
        }

        [HttpGet("Complexes-Detail-Blocks")]
        public async Task<ActionResult<IEnumerable<Get_Coplexes_Detail_BlocksDto>>> GetComplexWithDetailBlocks()
        {
            if (await _repository.complexIsNull())
            {
                return NotFound();
            }


            var complex = await _repository.GetComplexDetailBlock();

            if (!complex.Any())
            {
                return NotFound();
            }


            return complex;
        }




        [HttpGet("Complex-And-Number-Blocks/{id:int}")]
        public async Task<ActionResult<Get_Complex_And_CountBlock>> GetComplexAndBlock(int id)
        {
            if (await _repository.complexIsNull())
            {
                return NotFound();
            }


            var complex = await _repository.GetComplexCountBlocks(id);

            if (complex == null)
            {
                return NotFound();
            }

            return  complex;
        }

        [HttpGet("GetBy/{id:int}")]
        public async Task<ActionResult<Get_ComplexDto>> GetComplexList(int id)
        {
            if (await _repository.complexIsNull())
            {
                return NotFound();
            }


            var complex = await _repository.GetComplexById(id);
            
            if(complex == null)
            {
                return NotFound();
            }

            return complex;
        }



        [HttpGet("GetBy/{name}")]
        public async Task<ActionResult<List<Get_ComplexDto>>> GetComplexList( string name)
        {
            var complex = await _repository.FindComplexByName(name);

            if (!complex.Any())
            {
                return NotFound();
            }

            return  complex;
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

            _repository.AddComplex(complex);

            return CreatedAtAction(nameof(GetComplexList), new {id = complex.Id}, complex);
        }


        [HttpPatch]
        public async Task<IActionResult> PutComplex( int id , UpdateComplexDto complexDto)
        {
            var complex = await _repository.GetAllComplexWithUnits(id);

            if (complex == null)
            {
                return NotFound();
            }
            
            if (complex.Blocks.SelectMany(x => x.Units).Any())
            {
                return BadRequest("for this complex there is registered unit, you cant change number of units!");
            }

            complex.NumberUnits = complexDto.NumberUnits;

            _repository.SetEntry(complex);
          
            return Ok();
        }
    

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplex(int id)
        {
            if (await _repository.complexIsNull())
            {
                return NotFound();
            }

            var complex = await _repository.FindComplexById(id);

            if (complex == null)
            {
                return NotFound();
            }

            _repository.RemoveComplex(complex);

            return Ok();
        }

   


    }
}
