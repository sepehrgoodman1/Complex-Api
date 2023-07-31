using Ef.Persistence.ComplexProject.Units;
using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Blocks.Contracts.Dtos;
using Services.Blocks.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef.Persistence.ComplexProject.Blocks
{
    public class EfBlockRepository : BlockRepository
    {
        private readonly EFDataContext _context;

        public EfBlockRepository(EFDataContext context)
        {
            _context = context;
        }

        public bool BlocksExist()
        {
            if(_context.Block == null)
            {
                return false;
            }
            return true;
        }
        public async Task<List<Get_BlocksDto>> GetAllBlocks()
        {
            var block = from b in _context.Block
                        select new Get_BlocksDto()
                        {
                            Name = b.Name,
                            NumberUnits = b.NumberUnits,
                            RegisteredUnits = b.Units.Count(),
                            NotRegistedredUnits = b.NumberUnits - b.Units.Count()
                        };

            return await block.ToListAsync();
        }

        public async Task<Get_One_BlockDto> GetBlocksById(int id)
        {
            var block = await _context.Block.Select(b =>
                                                  new Get_One_BlockDto()
                                                  {
                                                      Id = b.Id,
                                                      Name = b.Name,
                                                      UnitDetails = b.Units.Select(u => new { u.Tenant, u.TypeHouse })
                                                  }).SingleOrDefaultAsync(c => c.Id == id);

            return block;
        }
        public async Task<List<Get_BlocksDto>> FindBlockByName(string name)
        {
            IQueryable<Block> query = _context.Block;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }
            var blocks = from b in query
                          select new Get_BlocksDto()
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


        public async void AddBlock(Block block)
        {
            _context.Block.Add(block);
            await _context.SaveChangesAsync();
        }

        public async Task<Block> FindBlock(int BlockId)
        {
            var block = await _context.Block.Include(b => b.Units).FirstOrDefaultAsync(x => x.Id == BlockId);
            return block;
        }

        public void Update(Block block)
        {
            _context.Update(block);

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

        public async void SaveBlock()
        {
            await _context.SaveChangesAsync();
        }
    }
}
