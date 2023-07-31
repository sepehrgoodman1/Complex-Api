using System.ComponentModel.DataAnnotations;

namespace Services.Dtos.Block
{
    public class Update_BlockDto
    {
        public string Name { get; set; }

        public int NumberUnits { get; set; }
    }
}
