namespace Entity.Entyties
{
    public class Complex
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberUnits { get; set; }
        public ICollection<Block> Blocks { get; set; } = new List<Block>();
    }
}
