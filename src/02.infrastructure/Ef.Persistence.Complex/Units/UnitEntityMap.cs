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
    public class UnitEntityMap : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> _)
        {
            _.ToTable("Units");
            _.HasKey(x => x.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd();
            _.Property(_ => _.Tenant).IsRequired();
            _.Property(_ => _.TypeHouse).IsRequired();
            _.HasOne(_ => _.Blocks)
                  .WithMany(_ => _.Units).HasForeignKey(_ => _.BlockId);
        }
    }
}
