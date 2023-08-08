using ComplexProject.Entities.Entyties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexProject.Persistence.Ef.Blocks
{
    public class ComplexEntityMap : IEntityTypeConfiguration<Complex>
    {
        public void Configure(EntityTypeBuilder<Complex> _)
        {
            _.ToTable("Complexes");
            _.HasKey(x => x.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd();
            _.Property(_ => _.Name).IsRequired();
            _.Property(_ => _.NumberUnits).IsRequired();
        }
    }
}
