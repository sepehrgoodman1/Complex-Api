using System.ComponentModel.DataAnnotations;

namespace Services.Units.Contracts.Dtos
{
    public class AddUnitDto
    {
        [Required] public string Tenant { get; set; }
        [Required] public string TypeHouse { get; set; }
        [Required] public int BlockId { get; set; }
    }
}
