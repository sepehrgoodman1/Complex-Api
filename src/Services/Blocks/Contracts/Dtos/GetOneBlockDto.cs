using System.Text.Json.Serialization;

namespace Services.Blocks.Contracts.Dtos
{
    public class GetOneBlockDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public object UnitDetails { get; set; }

    }
}
