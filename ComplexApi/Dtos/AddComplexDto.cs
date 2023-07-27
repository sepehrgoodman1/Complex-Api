using System.ComponentModel.DataAnnotations;

namespace ComplexApi.Dtos
{
    public class AddComplexDto
    {
        [Required] public string Name { get; set; }
        [Required] public int NumberUnits { get; set; }
    }
}
