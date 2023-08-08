using ComplexProject.Services.Units.Contracts;
using ComplexProject.Services.Units.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace ComplexProject.RestApi.Controllers
{
    [Route("api/units")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly UnitService _service;

        public UnitsController(UnitService services)
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

        }

    }
}
