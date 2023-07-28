using ComplexApi.ComplexApi;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace ComplexApi.Dtos
{
    public class Get_One_BlockDto
    {
        [JsonIgnoreAttribute]
        public int Id { get; set; }
        public string Name { get; set; }
        public object UnitDetails { get; set; }

    }
}
