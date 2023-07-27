namespace ComplexApi.ComplexApi
{
    public class Unit
    {
        public int Id { get; set; }
        public string Tenant { get; set; }
        public string TypeHouse { get; set; }
        public int BlockId { get; set; }
        public Block Blocks { get; set; }
    }
}
