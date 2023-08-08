namespace ComplexProject.Services.Complexes.Contracts.Dtos
{
    public class GetComplexDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegisteredUnits { get; set; }
        public int NotRegistedredUnits { get; set; }

    }
}
