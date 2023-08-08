using ComplexProject.Entities.Entyties;
using Microsoft.EntityFrameworkCore;

namespace ComplexProject.Persistence.Ef
{
    public class EFDataContext :DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options): base(options) { }
        
        public DbSet<Complex> Complexes { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Unit> Units { get; set; }
    }
}
