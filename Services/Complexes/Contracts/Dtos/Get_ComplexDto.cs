﻿using System.Text.Json.Serialization;

namespace Services.Complexes.Contracts.Dtos
{
    public class Get_ComplexDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegisteredUnits { get; set; }
        public int NotRegistedredUnits { get; set; }

    }
}