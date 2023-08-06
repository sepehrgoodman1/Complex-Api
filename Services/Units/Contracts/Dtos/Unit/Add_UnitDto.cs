﻿using System.ComponentModel.DataAnnotations;

namespace Services.Units.Contracts.Dtos.Unit
{
    public class Add_UnitDto
    {
        [Required] public string Tenant { get; set; }
        [Required] public string TypeHouse { get; set; }
        [Required] public int BlockId { get; set; }
    }
}