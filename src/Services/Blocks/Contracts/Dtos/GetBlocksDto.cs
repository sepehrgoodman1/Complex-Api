namespace Services.Blocks.Contracts.Dtos
{
    public class GetBlocksDto
    {
        public string Name { get; set; }
        public int NumberUnits { get; set; }
        public int RegisteredUnits { get; set; }
        public int NotRegistedredUnits { get; set; }

    }
}
