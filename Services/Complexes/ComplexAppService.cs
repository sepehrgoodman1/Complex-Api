using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Services.Complexes.Contracts;
using Services.Complexes.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Complexes
{
    public class ComplexAppService: ComplexService
    {
        private readonly ComplexRepository _repository;

        public ComplexAppService(ComplexRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Get_ComplexDto>> GetAll_WithRegUnit()
        {
            if (await _repository.complexIsNull())
            {
                /*return NotFound();*/
            }
            var complex = await _repository.GetComplexRegUnit();

            if (!complex.Any())
            {
                /*return NotFound();*/
            }

            return complex;
        }
        public async Task<List<Get_Coplexes_Detail_BlocksDto>> GetAll_WithBlockDetail()
        {
            if (await _repository.complexIsNull())
            {
               /* return NotFound();*/
            }

            var complex = await _repository.GetComplexDetailBlock();

            if (!complex.Any())
            {
             /*   return NotFound();*/
            }

            return complex;
        }
        public async Task<Get_Complex_And_CountBlock> GetById_WithNumBlocks(int id)
        {
            if (await _repository.complexIsNull())
            {
                /*return NotFound();*/
            }
            var complex = await _repository.GetComplexCountBlocks(id);

            if (complex == null)
            {
                /*return NotFound();*/
            }
            return complex;

        }
        public async Task<Get_ComplexDto> GetById(int id)
        {
            if (await _repository.complexIsNull())
            {
                /*return NotFound();*/
            }


            var complex = await _repository.GetComplexById(id);

            if (complex == null)
            {
                /*return NotFound();*/
            }

            return complex;
        }
        public async Task<List<Get_ComplexDto>> GetByName(string name)
        {
            var complex = await _repository.FindComplexByName(name);

            if (!complex.Any())
            {
                /*return NotFound();*/
            }

            return complex;
        }
        public  void Add(AddComplexDto dto)
        {
            var complex = new Complex
            {
                Name = dto.Name,
                NumberUnits = dto.NumberUnits
            };

            if (complex.NumberUnits < 4 || complex.NumberUnits > 1000)
            {
                /*return BadRequest("Number of Units Must be between 4 and 1000");*/
            }

            _repository.AddComplex(complex);

       /*     return CreatedAtAction(nameof(GetComplexList), new { id = complex.Id }, complex);*/
        }
        public async void Update(int id, UpdateComplexDto complexDto)
        {
            var complex = await _repository.GetAllComplexWithUnits(id);

            if (complex == null)
            {
                /*return NotFound();*/
            }

            if (complex.Blocks.SelectMany(x => x.Units).Any())
            {
               /* return BadRequest("for this complex there is registered unit, you cant change number of units!");*/
            }

            complex.NumberUnits = complexDto.NumberUnits;
            _repository.Update(complex);
        }
        public async void Delete(int id)
        {
            if (await _repository.complexIsNull())
            {
/*                return NotFound();*/
            }

            var complex = await _repository.FindComplexById(id);

            if (complex == null)
            {
             /*   return NotFound();*/
            }

            _repository.Remove(complex);

        }

    }
}
