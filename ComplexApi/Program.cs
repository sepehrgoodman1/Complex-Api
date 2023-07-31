using Ef.Persistence.ComplexProject;
using Ef.Persistence.ComplexProject.Blocks;
using Ef.Persistence.ComplexProject.Complexes;
using Ef.Persistence.ComplexProject.Units;
using Microsoft.EntityFrameworkCore;
using Services.Blocks.Contracts;
using Services.Complexes.Contracts;
using Services.Units.Contracts;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EFDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ComplexRepository, EfComplexRepository>();
builder.Services.AddScoped<BlockRepository, EfBlockRepository>();
builder.Services.AddScoped<UnitRepository, EfUnitRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
      .AddJsonOptions(options =>
      {
          options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
