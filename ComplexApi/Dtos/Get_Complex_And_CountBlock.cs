﻿using ComplexApi.ComplexApi;

namespace ComplexApi.Dtos
{
    public class Get_Complex_And_CountBlock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegisteredUnits { get; set; }
        public int NotRegistedredUnits { get; set; }
        public int NumberOfBlocks { get; set; }

    }
}
