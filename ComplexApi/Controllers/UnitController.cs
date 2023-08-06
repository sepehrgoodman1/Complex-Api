using Microsoft.AspNetCore.Mvc;
using Services.Units.Contracts;
using Services.Units.Contracts.Dtos.Unit;

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
        public async Task<List<Get_UnitsDto>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPost]
        public async void Add(Add_UnitDto dto)
        {
            _service.Add(dto);

          /*  return CreatedAtAction(nameof(GetUnitList), new { id = unit.Id }, unit);*/
        }

    }
}
