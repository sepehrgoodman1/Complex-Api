using Microsoft.AspNetCore.Mvc;
using Services.Complexes.Contracts.Dtos;
using Services.Complexes.Contracts;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexController : ControllerBase
    {
        private readonly ComplexService _service;

        public ComplexController(ComplexService service)
        {
            _service = service;
        }

        [HttpGet("Complexes-With-RegisteredUnit")]
        public async Task<List<Get_ComplexDto>> GetComplexList()
        {
            return  await _service.GetAll_WithRegUnit();
        }

        [HttpGet("Complexes-Detail-Blocks")]
        public async Task<List<Get_Coplexes_Detail_BlocksDto>> GetComplexWithDetailBlocks()
        {
            return await _service.GetAll_WithBlockDetail();
        }

        [HttpGet("Complex-And-Number-Blocks/{id:int}")]
        public async Task<Get_Complex_And_CountBlock> GetComplexAndBlock(int id)
        {
            return await _service.GetById_WithNumBlocks(id);
        }

        [HttpGet("GetBy/{id:int}")]
        public async Task<Get_ComplexDto> GetComplexList(int id)
        {
            return await _service.GetById(id);
        }

        [HttpGet("GetBy/{name}")]
        public async Task<List<Get_ComplexDto>> GetComplexList( string name)
        {
            return await _service.GetByName(name);
        }



        [HttpPost]
        public  void PostComplex(AddComplexDto dto)
        {
            _service.Add(dto);
        }


        [HttpPatch]
        public  void PutComplex( int id , UpdateComplexDto complexDto)
        {
            _service.Update(id, complexDto);
        }
    

        [HttpDelete("{id}")]
        public async void DeleteComplex(int id)
        {
            _service.Delete(id);
        }

   


    }
}
