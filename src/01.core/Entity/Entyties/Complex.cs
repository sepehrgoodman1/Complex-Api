namespace ComplexProject.Entities.Entyties
{
    public class Complex
    {
        public Complex()
        {
            Blocks = new();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberUnits { get; set; }
        public HashSet<Block> Blocks { get; set; } 
    }
}
