using ComplexProject.Entities.Entyties;
using ComplexProject.Persistence.Ef;
using ComplexProject.Persistence.Ef.Blocks;
using ComplexProject.Persistence.Ef.Complexes;
using ComplexProject.Persistence.Ef.Units;
using ComplexProject.Services.Blocks;
using ComplexProject.Services.Blocks.Contracts;
using ComplexProject.Services.Blocks.Contracts.Dtos;
using ComplexProject.Services.Blocks.Exceptions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Templete.TestTools.DataBaseConfig.Unit;

namespace ComplexProject.Units.Tests.Blocks
{
    public class BlockServiceTest : BusinessUnitTest
    {
        private readonly BlockService _sut;
        public BlockServiceTest()
        {
            var blockRepository = new EfBlockRepository(SetupContext);
            var unitOfWork = new EFUnitOfWork(SetupContext);
            var complexRepository = new EfComplexRepository(SetupContext);
            var unitReository = new EfUnitRepository(SetupContext);

            _sut = new BlockAppService(blockRepository, unitOfWork, complexRepository, unitReository);
        }

        [Fact]
        public async Task Add()
        {
            //Arrange
            var complex = new Entities.Entyties.Complex
            {
                Name = "dummy",
                NumberUnits = 1,
            };
            Save(complex);
            var dto = new AddBlockDto
            {
                ComplexId = complex.Id,
                Name = "test",
                NumberUnits = 20
            };

            //Act 
            await _sut.Add(dto);

            //Assert
            var result = ReadContext.Blocks.Single();
            result.Name.Should().Be(dto.Name);
            result.ComplexId.Should().Be(dto.ComplexId);
            result.NumberUnits.Should().Be(dto.NumberUnits);
        }

        [Fact]
        public async Task add2()
        {
            var invalidComplexId = -1;
            var dto = new AddBlockDto
            {
                ComplexId = invalidComplexId,
                Name = "test",
                NumberUnits = 20
            };


            var result = () => _sut.Add(dto);

            await result.Should().ThrowAsync<InvalidComplexIdException>();
        }

        [Fact]
        public async Task Get()
        {
            var complex = new Entities.Entyties.Complex
            {
                Name = "dummy",
                NumberUnits = 1,
            };
            var block = new Block
            {
                Complex = complex,
                Name = "blcokName",
                NumberUnits = 2
            };
            Save(block);

            var result = await _sut.GetAll(new Pagination());

            result.Elements.Should().HaveCount(1);
            result.Elements.First().Name.Should().Be("blcokName");
        }


    }
}

