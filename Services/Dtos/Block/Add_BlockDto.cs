using System.ComponentModel.DataAnnotations;

namespace Services.Dtos.Block
{
    public class Add_BlockDto
    {
        [Required] public string Name { get; set; }
        [Required] public int NumberUnits { get; set; }
        [Required] public int ComplexId { get; set; }
    }
}
