using Microsoft.AspNetCore.Mvc;
using Services.Units.Contracts;
using Services.Units.Contracts.Dtos;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly UnitService _service;

        public UnitController(UnitService services)
        {
            _service = services;
        }
        [HttpGet]
        public async Task<List<GetUnitsDto>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPost]
        public async Task<int> Add(AddUnitDto dto)
        {
            return await _service.Add(dto);

          /*  return CreatedAtAction(nameof(GetUnitList), new { id = unit.Id }, unit);*/
        }

    }
}
