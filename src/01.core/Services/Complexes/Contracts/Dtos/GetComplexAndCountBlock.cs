namespace ComplexProject.Services.Complexes.Contracts.Dtos
{
    public class GetComplexAndCountBlock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegisteredUnits { get; set; }
        public int NotRegistedredUnits { get; set; }
        public int NumberOfBlocks { get; set; }

    }
}
