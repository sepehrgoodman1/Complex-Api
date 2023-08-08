using ComplexProject.Services.Complexes.Contracts;
using ComplexProject.Services.Complexes.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace ComplexProject.RestApi.Controllers
{
    [Route("api/complexes")]
    [ApiController]
    public class ComplexController : ControllerBase
    {
        private readonly ComplexService _service;

        public ComplexController(ComplexService service)
        {
            _service = service;
        }

        [HttpGet("Complexes-With-RegisteredUnit")]
        public async Task<List<GetComplexDto>> GetAllWithRegUnit()
        {
            return  await _service.GetAll_WithRegUnit();
        }

        [HttpGet("Complexes-Detail-Blocks")]
        public async Task<List<GetCoplexesDetailBlocksDto>> GetWithDetailBlocks()
        {
            return await _service.GetAll_WithBlockDetail();
        }

        [HttpGet("Complex-And-Number-Blocks/{id:int}")]
        public async Task<GetComplexAndCountBlock> GetWithNumberBlocks(int id)
        {
            return await _service.GetById_WithNumBlocks(id);
        }

        [HttpGet("GetBy/{id:int}")]
        public async Task<GetComplexDto> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpGet("GetBy/{name}")]
        public async Task<List<GetComplexDto>> GetByName( string name)
        {
            return await _service.GetByName(name);
        }



        [HttpPost]
        public async Task<int> Add(AddComplexDto dto)
        {
            return await _service.Add(dto);
        }


        [HttpPatch]
        public async Task Update( int id , UpdateComplexDto complexDto)
        {
           await _service.Update(id, complexDto);
        }
    

       

   


    }
}
