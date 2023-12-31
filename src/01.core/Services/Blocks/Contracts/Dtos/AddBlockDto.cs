﻿using System.ComponentModel.DataAnnotations;

namespace ComplexProject.Services.Blocks.Contracts.Dtos
{
    public class AddBlockDto
    {
        [Required] public string Name { get; set; }
        [Required] [Range(1,int.MaxValue)] public int NumberUnits { get; set; }
        [Required] public int ComplexId { get; set; }
    }
}
