using System.ComponentModel.DataAnnotations;

namespace ComplexApi.Dtos
{
    public class UpdateBlockDto
    {
        public string Name { get; set; }

        public int NumberUnits { get; set; }
    }
}
