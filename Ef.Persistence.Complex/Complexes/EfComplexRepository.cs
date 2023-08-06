using Entity.Entyties;
using Microsoft.EntityFrameworkCore;
using Services.Complexes.Contracts;
using Services.Complexes.Contracts.Dtos;

namespace Ef.Persistence.ComplexProject.Complexes
{
    public class EfComplexRepository : ComplexRepository
    {
        private readonly EFDataContext _context;

        public EfComplexRepository(EFDataContext context)
        {
            _context = context;
        }

      

        public async Task<List<GetComplexDto>> GetWithNumRegisteredUnits()
        {
            var complex = from c in _context.Complex
                          select new GetComplexDto()
                          {
                              Id = c.Id,
                              Name = c.Name,
                              RegisteredUnits = c.Blocks.SelectMany(u => u.Units).Count(),
                              NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count()
                          };

            return await complex.ToListAsync();
        }
        public async Task<List<GetCoplexesDetailBlocksDto>> GetComplexDetailBlock()
        {
            var complex = from c in _context.Complex
                          select new GetCoplexesDetailBlocksDto()
                          {
                              Name = c.Name,
                              BlockDetails = c.Blocks.Select(block => new { block.Name, block.NumberUnits })
                          };

            return await complex.ToListAsync();
        }

        public async Task<GetComplexAndCountBlock> GetComplexCountBlocks(int id)
        {
            var complex = await _context.Complex.Select(c =>
                                                   new GetComplexAndCountBlock()
                                                   {
                                                       Id = c.Id,
                                                       Name = c.Name,
                                                       RegisteredUnits = c.Blocks.SelectMany(u => u.Units).Count(),
                                                       NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count(),
                                                       NumberOfBlocks = c.Blocks.Count()
                                                   }).SingleOrDefaultAsync(b => b.Id == id);
            return  complex;
        }

        public async Task<GetComplexDto> GetAllWithRegUnit(int id)
        {
            var complex = await _context.Complex.Select(c =>
                                                new GetComplexDto()
                                                {
                                                    Id = c.Id,
                                                    Name = c.Name,
                                                    RegisteredUnits = c.Blocks.SelectMany(u => u.Units).Count(),
                                                    NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count()
                                                }).SingleOrDefaultAsync(b => b.Id == id);

            return complex;
        }

        public async Task<List<GetComplexDto>> FindByName(string name)
        {

            IQueryable<Complex> query = _context.Complex;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var complex = from c in query
                          select new GetComplexDto()
                          {
                              Id = c.Id,
                              Name = c.Name,
                              RegisteredUnits = c.Blocks.SelectMany(u => u.Units).Count(),
                              NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count()
                          };




            return await complex.ToListAsync();
        }

        public async void Add(Complex complex)
        {
            _context.Complex.Add(complex);
        }

        public async Task<Complex> GetComplexWithAllUnits(int id)
        {
            var complex = await _context.Complex.Include(c => c.Blocks).ThenInclude(c => c.Units).FirstOrDefaultAsync(x => x.Id == id);

            return complex;
        }
        public async Task Update(Complex complex)
        {
              _context.Update(complex);
        }

        public async Task<Complex> GetById(int id)
        {
            return await _context.Complex.FindAsync(id);
        }

     
    }
}
