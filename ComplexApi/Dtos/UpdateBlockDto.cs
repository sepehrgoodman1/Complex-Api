using System.ComponentModel.DataAnnotations;

namespace Apis.Dtos
{
    public class UpdateBlockDto
    {
        public string Name { get; set; }

        public int NumberUnits { get; set; }
    }
}
