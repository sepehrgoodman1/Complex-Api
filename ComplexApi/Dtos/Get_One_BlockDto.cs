using ComplexApi.ComplexApi;

namespace ComplexApi.Dtos
{
    public class Get_One_BlockDto
    {
        public string Name { get; set; }
        public ICollection<Unit> Units { get; set; }

    }
}
