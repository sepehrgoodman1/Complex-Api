using Entity.Entyties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Dtos.Block;
using Services.Dtos.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef.Persistence.ComplexProject.Complexes
{
    public class EfComplexRepository : IComplexRepository
    {
        private readonly EFDataContext _context;

       
        public async Task<bool> complexIsNull()
        {
            if ( _context.Complex.Any())
            {
                return true;
            }
            else return false;
        }

        public async Task<List<Get_ComplexDto>> GetComplexRegUnit()
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

        public async Task<Get_ComplexDto> GetComplexById(int id)
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

        public async Task<List<Get_ComplexDto>> FindComplexByName(string name)
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

        public async void AddComplex(Complex complex)
        {
            _context.Complex.Add(complex);

            await _context.SaveChangesAsync();
        }

        public async Task<Complex> GetAllComplexWithUnits(int id)
        {
            var complex = await _context.Complex.Include(c => c.Blocks).ThenInclude(c => c.Units).FirstAsync(x=>x.Id == id);

            return complex;
        }
        public void SetEntry(Complex complex)
        {
            _context.Entry(complex).State = EntityState.Modified;
            _context.SaveChangesAsync();

        }

        public async Task<Complex> FindComplexById(int id)
        {
            return await _context.Complex.FindAsync(id);
        }

        public async void RemoveComplex(Complex complex)
        {
            _context.Complex.Remove(complex);
            await _context.SaveChangesAsync();
        }
    }
}
