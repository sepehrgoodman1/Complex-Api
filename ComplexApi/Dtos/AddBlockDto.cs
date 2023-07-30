using System.ComponentModel.DataAnnotations;

namespace Apis.Dtos
{
    public class AddBlockDto
    {
        [Required] public string Name { get; set; }
        [Required] public int NumberUnits { get; set; }
        [Required] public int ComplexId { get; set; }
    }
}
