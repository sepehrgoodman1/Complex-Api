using ComplexApi.ComplexApi;
using Microsoft.EntityFrameworkCore;

namespace ComplexApi.ComplexApi
{
    public class EFDataContext :DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options): base(options) { }
        
        public DbSet<Complex> Complex { get; set; }
        public DbSet<Block> Block { get; set; }
        public DbSet<Unit> Unit { get; set; }
    }
}
