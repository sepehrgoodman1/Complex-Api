
using ComplexProject.Persistence.Ef;
using ComplexProject.Services.Blocks.Contracts;
using ComplexProject.Services.Blocks.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

using Taav.Contracts.Interfaces;

namespace ComplexProject.RestApi.Controllers
{
    [Route("api/blocks")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly BlockService _service;

        public BlockController(BlockService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IPageResult<GetBlocksDto>> GetAll([FromQuery] Pagination? pagination)
        {
            return await _service.GetAll(pagination);
        }

        [HttpGet("{id:int}")]
        public async Task<GetOneBlockDto> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpGet("{name}")]
        public async Task<List<GetBlocksDto>> GetByName([FromQuery] string name)
        {
            return await _service.GetByName(name);
        }

        [HttpPost]
        public async Task<int> Add(AddBlockDto dto)
        {
            return await _service.Add(dto);
        }

        [HttpPatch]
        public async Task Update(int id, UpdateBlockDto BlockDto)
        {
            await _service.Update(id, BlockDto);
        }
   
    }
}
