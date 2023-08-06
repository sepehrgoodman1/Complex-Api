using Entity.Entyties;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Complexes.Contracts;
using Services.Complexes.Contracts.Dtos;
using Services.Complexes.Exceptions;
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
            if (await _repository.IsNull())
            {
                throw new ComplexNotFoundException();
            }
            var complex = await _repository.GetWithNumRegisteredUnits();

            if (!complex.Any())
            {
                throw new ComplexNotFoundException();
            }

            return complex;
        }
        public async Task<List<Get_Coplexes_Detail_BlocksDto>> GetAll_WithBlockDetail()
        {
            if (await _repository.IsNull())
            {
                throw new ComplexNotFoundException();
            }

            var complex = await _repository.GetComplexDetailBlock();

            if (!complex.Any())
            {
                throw new ComplexNotFoundException();
            }

            return complex;
        }
        public async Task<Get_Complex_And_CountBlock> GetById_WithNumBlocks(int id)
        {
            if (await _repository.IsNull())
            {
                throw new ComplexNotFoundException();
            }
            var complex = await _repository.GetComplexCountBlocks(id);

            if (complex == null)
            {
                throw new ComplexNotFoundException();
            }
            return complex;

        }
        public async Task<Get_ComplexDto> GetById(int id)
        {
            if (await _repository.IsNull())
            {
                throw new ComplexNotFoundException();
            }


            var complex = await _repository.GetAllWithRegUnit(id);

            if (complex == null)
            {
                throw new ComplexNotFoundException();
            }

            return complex;
        }
        public async Task<List<Get_ComplexDto>> GetByName(string name)
        {
            var complex = await _repository.FindByName(name);

            if (!complex.Any())
            {
                throw new ComplexNotFoundException();
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
                throw new NumberOfUnitsException();
            }

            _repository.Add(complex);

       /*     return CreatedAtAction(nameof(GetComplexList), new { id = complex.Id }, complex);*/
        }
        public async void Update(int id, UpdateComplexDto complexDto)
        {
            var complex = await _repository.GetComplexWithAllUnits(id);

            if (complex == null)
            {
                throw new ComplexNotFoundException();
            }

            if (complex.Blocks.SelectMany(x => x.Units).Any())
            {
                throw new RegisteredUnitException();
               /* return BadRequest("for this complex there is registered unit, you cant change number of units!");*/
            }

            complex.NumberUnits = complexDto.NumberUnits;
            _repository.Update(complex);
        }
        public async void Delete(int id)
        {
            if (await _repository.IsNull())
            {
                throw new ComplexNotFoundException();
            }

            var complex = await _repository.GetById(id);

            if (complex == null)
            {
                throw new ComplexNotFoundException();
            }

            _repository.Remove(complex);

        }

    }
}
