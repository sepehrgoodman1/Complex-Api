﻿using System.ComponentModel.DataAnnotations;

namespace ComplexProject.Services.Complexes.Contracts.Dtos
{
    public class AddComplexDto
    {
        [Required] public string Name { get; set; }
        [Required] public int NumberUnits { get; set; }
    }
}
