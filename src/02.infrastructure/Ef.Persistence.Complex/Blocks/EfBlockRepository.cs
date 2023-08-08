using Microsoft.EntityFrameworkCore;

using Taav.Contracts.Interfaces;
using ComplexProject.Services.Blocks.Contracts;
using ComplexProject.Services.Blocks.Contracts.Dtos;
using ComplexProject.Entities.Entyties;
using System.Reflection.Metadata.Ecma335;

namespace ComplexProject.Persistence.Ef.Blocks
{
    public class EfBlockRepository : BlockRepository
    {
        private readonly DbSet<Block> _blocks;

        public EfBlockRepository(EFDataContext context)
        {
            _blocks = context.Blocks;
        }


        public async Task<IPageResult<GetBlocksDto>> GetAll(IPagination? pagination)
        {
            var block = _blocks.Select(_ => new GetBlocksDto
            {

                Name = _.Name,
                NumberUnits = _.NumberUnits,
                RegisteredUnits = _.Units.Count(),
                NotRegistedredUnits = _.NumberUnits - _.Units.Count()


            });




            return await block.Paginate(pagination);
        }

        public async Task<GetOneBlockDto> GetById(int id)
        {
            var block = await _blocks.Select(b =>
                                                  new GetOneBlockDto()
                                                  {
                                                      Id = b.Id,
                                                      Name = b.Name,
                                                      UnitDetails = b.Units.Select(u => new { u.Tenant, u.TypeHouse })
                                                  }).SingleOrDefaultAsync(c => c.Id == id);

            return block;
        }
        public async Task<List<GetBlocksDto>> FindByName(string? name)
        {


            IQueryable<Block> query = _blocks;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }
            var blocks = from b in query
                         select new GetBlocksDto()
                         {
                             Name = b.Name,
                             NumberUnits = b.NumberUnits,
                             RegisteredUnits = b.Units.Count(),
                             NotRegistedredUnits = b.NumberUnits - b.Units.Count()
                         };


            return await blocks.ToListAsync();
        }

      


        public async void Add(Block block)
        {
            _blocks.Add(block);
        }

        public async Task<Block> FindBlock(int BlockId)
        {
            var block = await _blocks.Include(b => b.Units).FirstOrDefaultAsync(x => x.Id == BlockId);
            return block;
        }

        public async Task Update(Block block)
        {
            _blocks.Update(block);

        }
        public async Task<bool> BlockIdDoesExist(int blockId)
        {
            return await _blocks.AnyAsync(x => x.Id == blockId);
        }

        public async Task<bool> ExistUnitNameInBlock(int blockId, string tenant)
        {
            var block = await _blocks.Include(b => b.Units).FirstOrDefaultAsync(x => x.Id == blockId);

            return block.Units.Select(b => b.Tenant).Contains(tenant);
        }

        public async Task<bool> IsExistByNameAndComplexId(int complexId, string name)
        {
            return await _blocks.AnyAsync(_ => _.ComplexId == complexId && _.Name == name);
        }
    }
}