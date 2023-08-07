using Entity.Entyties;
using Microsoft.EntityFrameworkCore;
using Services.Blocks.Contracts.Dtos;
using Services.Blocks.Contracts;
using Taav.Contracts.Interfaces;

namespace Ef.Persistence.ComplexProject.Blocks
{
    public class EfBlockRepository : BlockRepository
    {
        private readonly EFDataContext _context;

        public EfBlockRepository(EFDataContext context)
        {
            _context = context;
        }

     
        public async Task<IPageResult<GetBlocksDto>> GetAll(IPagination? pagination)
        {
            var block =  from b in _context.Block
                        select new GetBlocksDto()
                        {
                            Name = b.Name,
                            NumberUnits = b.NumberUnits,
                            RegisteredUnits = b.Units.Count(),
                            NotRegistedredUnits = b.NumberUnits - b.Units.Count()
                        };

           /* 2.IsGreaterThen(5);*/

            return await block.Paginate(pagination);
        }

        public async Task<GetOneBlockDto> GetById(int id)
        {
            var block = await _context.Block.Select(b =>
                                                  new GetOneBlockDto()
                                                  {
                                                      Id = b.Id,
                                                      Name = b.Name,
                                                      UnitDetails = b.Units.Select(u => new { u.Tenant, u.TypeHouse })
                                                  }).SingleOrDefaultAsync(c => c.Id == id);

            return block;
        }
        public async Task<List<GetBlocksDto>> FindByName(string name)
        {
            IQueryable<Block> query = _context.Block;

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

        public async Task<bool> ComplexIdExist(int BlockId)
        {
            return  _context.Complex.Any(x => x.Id == BlockId);
        }

        public async Task<bool> CheckBlockName(int complexId, string blockName)
        {
            var complex = await _context.Complex.Include(b => b.Blocks).FirstOrDefaultAsync(x => x.Id == complexId);

            if (complex.Blocks.Select(b => b.Name).Contains(blockName))
            {
                return true;
            }
            else return false;
        }


        public async void Add(Block block)
        {
            _context.Block.Add(block);
        }

        public async Task<Block> FindBlock(int BlockId)
        {
            var block = await _context.Block.Include(b => b.Units).FirstOrDefaultAsync(x => x.Id == BlockId);
            return block;
        }

        public async Task Update(Block block)
        {
             _context.Block.Update(block);

        }
        public async Task<bool> CheckUnits(int BlockId)
        {
            var listUnits = await _context.Unit.ToListAsync();

            int CounterUnits = 0;

            foreach (var unit in listUnits)
            {
                if (BlockId == unit.BlockId)
                {
                    CounterUnits++;
                }
            }
            if (CounterUnits > 0)
            {
                return true;
            }
            else return false;
        }

      
    }
}
