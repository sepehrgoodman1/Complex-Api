using ComplexProject.Entities.Entyties;
using ComplexProject.Services.Complexes.Contracts;
using ComplexProject.Services.Complexes.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;


namespace ComplexProject.Persistence.Ef.Complexes
{
    public class EfComplexRepository : ComplexRepository
    {
        private readonly DbSet<Complex> _complexes;

        public EfComplexRepository(EFDataContext context)
        {
            _complexes = context.Complexes;
        }

      

        public async Task<List<GetComplexDto>> GetWithNumRegisteredUnits()
        {
            var complex = from c in _complexes
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
            var complex = from c in _complexes
                          select new GetCoplexesDetailBlocksDto()
                          {
                              Name = c.Name,
                              BlockDetails = c.Blocks.Select(block => new { block.Name, block.NumberUnits })
                          };

            return await complex.ToListAsync();
        }

        public async Task<GetComplexAndCountBlock> GetComplexCountBlocks(int id)
        {
            var complex = await _complexes.Select(c =>
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
            var complex = 
                await _complexes.Select(c =>

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

            IQueryable<Complex> query = _complexes;

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

        public  void Add(Complex complex)
        {
            _complexes.Add(complex);
        }

        public async Task<Complex> GetComplexWithAllUnits(int id)
        {
            var complex = await _complexes.Include(c => c.Blocks).ThenInclude(c => c.Units).FirstOrDefaultAsync(x => x.Id == id);

            return complex;
        }
        public async Task Update(Complex complex)
        {
              _complexes.Update(complex);
        }

        public async Task<Complex> GetById(int id)
        {
            return await _complexes.FindAsync(id);
        }

        public async Task<bool> ComplexIdExist(int BlockId)
        {
            return _complexes.Any(x => x.Id == BlockId);
        }

     


    }
}
