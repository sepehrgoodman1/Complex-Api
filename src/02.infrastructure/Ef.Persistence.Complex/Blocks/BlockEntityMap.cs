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
    public class BlockEntityMap : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> _)
        {
            _.ToTable("Blocks");
            _.HasKey(x => x.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd();
            _.Property(_ => _.Name).IsRequired();
            _.Property(_ => _.NumberUnits).IsRequired();
            _.HasOne(_ => _.Complex)
                .WithMany(_ => _.Blocks).HasForeignKey(_ => _.ComplexId);
        }
    }
}
