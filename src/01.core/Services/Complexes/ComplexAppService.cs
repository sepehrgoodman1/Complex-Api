
using ComplexProject.Entities.Entyties;
using ComplexProject.Services.Complexes.Contracts;
using ComplexProject.Services.Complexes.Contracts.Dtos;
using ComplexProject.Services.Complexes.Exceptions;
using System.Collections.Generic;
using Taav.Contracts.Interfaces;

namespace ComplexProject.Services.Complexes
{
    public class ComplexAppService : ComplexService
    {
        private readonly ComplexRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public ComplexAppService(
            ComplexRepository repository,
            UnitOfWork unitOfWork)
        {
            _repository = repository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<GetComplexDto>> GetAll_WithRegUnit()
        {
            var complex = await _repository.GetWithNumRegisteredUnits();

            if (!complex.Any())
            {
                throw new ComplexNotFoundException();
            }

            return complex;
        }
        public async Task<List<GetCoplexesDetailBlocksDto>> GetAll_WithBlockDetail()
        {
            var complex = await _repository.GetComplexDetailBlock();

            if (!complex.Any())
            {
                throw new ComplexNotFoundException();
            }

            return complex;
        }
        public async Task<GetComplexAndCountBlock> GetById_WithNumBlocks(int id)
        {
            var complex = await _repository.GetComplexCountBlocks(id);

            if (complex == null)
            {
                throw new ComplexNotFoundException();
            }
            return complex;

        }
        public async Task<GetComplexDto> GetById(int id)
        {

            var complex = await _repository.GetAllWithRegUnit(id);

            if (complex == null)
            {
                throw new ComplexNotFoundException();
            }

            return complex;
        }
        public async Task<List<GetComplexDto>> GetByName(string name)
        {
            var complex = await _repository.FindByName(name);

            if (!complex.Any())
            {
                throw new ComplexNotFoundException();
            }

            return complex;
        }
        public async Task<int> Add(AddComplexDto dto)
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
            await _unitOfWork.Complete();
            return complex.Id;
            /*     return CreatedAtAction(nameof(GetComplexList), new { id = complex.Id }, complex);*/
        }
        public async Task Update(int id, UpdateComplexDto complexDto)
        {
            var complex = await _repository.GetComplexWithAllUnits(id);

            if (complex == null)
            {
                throw new ComplexNotFoundException();
            }

            if (complex.Blocks.SelectMany(x => x.Units).Any())
            {
                throw new RegisteredUnitException();
/*                return BadRequest("for this complex there is registered unit, you cant change number of units!");
*/            }

            complex.NumberUnits = complexDto.NumberUnits;

            _repository.Update(complex);
            await _unitOfWork.Complete();
        }
      
        

    }
}
