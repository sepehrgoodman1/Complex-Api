namespace ComplexApi.ComplexApi
{
    public class Block
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberUnits { get; set; }
        public int ComplexId { get; set; }
        public Complex Complexes { get; set; }
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
