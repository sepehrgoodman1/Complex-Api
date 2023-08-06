using Entity.Entyties;
using Services.Complexes.Contracts.Dtos;

namespace Services.Complexes.Contracts
{
    public interface ComplexRepository
    {
        Task<List<GetComplexDto>> GetWithNumRegisteredUnits();
        Task<List<GetCoplexesDetailBlocksDto>> GetComplexDetailBlock();
        Task<GetComplexAndCountBlock> GetComplexCountBlocks(int id);
        Task<GetComplexDto> GetAllWithRegUnit(int id);
        Task<List<GetComplexDto>> FindByName(string name);
        void Add(Complex complex);
        Task<Complex> GetComplexWithAllUnits(int id);
        Task Update(Complex complex);
        Task<Complex> GetById(int id);
    }
}
