using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Complexes.Contracts;
using Services.Complexes.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef.Persistence.ComplexProject.Complexes
{
    public class EfComplexRepository : ComplexRepository
    {
        private readonly EFDataContext _context;

        public EfComplexRepository(EFDataContext context)
        {
            _context = context;
        }

        public async Task<bool> IsNull()
        {
            if ( _context.Complex== null)
            {
                return true;
            }
            else return false;
        }

        public async Task<List<Get_ComplexDto>> GetWithNumRegisteredUnits()
        {
            var complex = from c in _context.Complex
                          select new Get_ComplexDto()
                          {
                              Id = c.Id,
                              Name = c.Name,
                              RegisteredUnits = c.Blocks.SelectMany(u => u.Units).Count(),
                              NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count()
                          };

            return await complex.ToListAsync();
        }
        public async Task<List<Get_Coplexes_Detail_BlocksDto>> GetComplexDetailBlock()
        {
            var complex = from c in _context.Complex
                          select new Get_Coplexes_Detail_BlocksDto()
                          {
                              Name = c.Name,
                              BlockDetails = c.Blocks.Select(block => new { block.Name, block.NumberUnits })
                          };

            return await complex.ToListAsync();
        }

        public async Task<Get_Complex_And_CountBlock> GetComplexCountBlocks(int id)
        {
            var complex = await _context.Complex.Select(c =>
                                                   new Get_Complex_And_CountBlock()
                                                   {
                                                       Id = c.Id,
                                                       Name = c.Name,
                                                       RegisteredUnits = c.Blocks.SelectMany(u => u.Units).Count(),
                                                       NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count(),
                                                       NumberOfBlocks = c.Blocks.Count()
                                                   }).SingleOrDefaultAsync(b => b.Id == id);
            return  complex;
        }

        public async Task<Get_ComplexDto> GetAllWithRegUnit(int id)
        {
            var complex = await _context.Complex.Select(c =>
                                                new Get_ComplexDto()
                                                {
                                                    Id = c.Id,
                                                    Name = c.Name,
                                                    RegisteredUnits = c.Blocks.SelectMany(u => u.Units).Count(),
                                                    NotRegistedredUnits = c.NumberUnits - c.Blocks.SelectMany(u => u.Units).Count()
                                                }).SingleOrDefaultAsync(b => b.Id == id);

            return complex;
        }

        public async Task<List<Get_ComplexDto>> FindByName(string name)
        {

            IQueryable<Complex> query = _context.Complex;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var complex = from c in query
                          select new Get_ComplexDto()
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

            await _context.SaveChangesAsync();
        }

        public async Task<Complex> GetComplexWithAllUnits(int id)
        {
            var complex = await _context.Complex.Include(c => c.Blocks).ThenInclude(c => c.Units).FirstAsync(x=>x.Id == id);

            return complex;
        }
        public void Update(Complex complex)
        {
            _context.Update(complex);
            _context.SaveChangesAsync();

        }

        public async Task<Complex> GetById(int id)
        {
            return await _context.Complex.FindAsync(id);
        }

        public async void Remove(Complex complex)
        {
            _context.Complex.Remove(complex);
            await _context.SaveChangesAsync();
        }
    }
}
