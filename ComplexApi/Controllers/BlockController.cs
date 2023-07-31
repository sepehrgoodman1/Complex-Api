
using Microsoft.AspNetCore.Mvc;
using Services.Blocks.Contracts;
using Services.Blocks.Contracts.Dtos;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly BlockService _service;

        public BlockController(BlockService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Get_BlocksDto>> GetBlockList()
        {
            return await _service.GetAll();
        }

        [HttpGet("GetBy/{id:int}")]
        public async Task<Get_One_BlockDto> GetBlock(int id)
        {
            return await _service.GetById(id);
        }

        [HttpGet("GetBy/{name}")]
        public async Task<List<Get_BlocksDto>> GetBlockist(string name)
        {
            return await _service.GetByName(name);
        }

        [HttpPost]
        public async void PostBlock(Add_BlockDto dto)
        {
            _service.Add(dto);
        }

        [HttpPatch]
        public async void UpdateBlock(int id, Update_BlockDto BlockDto)
        {
            _service.Update(id, BlockDto);
        }
   
    }
}
