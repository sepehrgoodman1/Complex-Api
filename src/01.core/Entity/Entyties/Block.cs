namespace ComplexProject.Entities.Entyties
{
    public class Block
    {
        public Block()
        {
            Units = new();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberUnits { get; set; }
        public int ComplexId { get; set; }
        public Complex Complex { get; set; }
        public HashSet<Unit> Units { get; set; }
    }
}
