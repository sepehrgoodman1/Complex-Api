using System.ComponentModel.DataAnnotations;

namespace Services.Blocks.Contracts.Dtos
{
    public class Update_BlockDto
    {
        public string Name { get; set; }

        public int NumberUnits { get; set; }
    }
}
