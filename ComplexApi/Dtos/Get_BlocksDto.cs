using ComplexApi.ComplexApi;

namespace Apis.Dtos
{
    public class Get_BlocksDto
    {
        public string Name { get; set; }
        public int NumberUnits { get; set; }
        public int RegisteredUnits { get; set; }
        public int NotRegistedredUnits { get; set; }

    }
}
