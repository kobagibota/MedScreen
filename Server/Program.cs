using BaseLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using Server.Services;
using ServerLibrary.Data;
using ServerLibrary.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Add application services.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILaboratoryService, LaboratoryService>();
builder.Services.AddScoped<IQCActionService, QCActionService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMethodService, MethodService>();
builder.Services.AddScoped<IStandardService, StandardService>();
builder.Services.AddScoped<IStrainGroupService, StrainGroupService>();
builder.Services.AddScoped<IStrainService, StrainService>();
builder.Services.AddScoped<ITestTypeService, TestTypeService>();
builder.Services.AddScoped<ITestQCService, TestQCService>();
builder.Services.AddScoped<ISupplyService, SupplyService>();
builder.Services.AddScoped<ILotTestService, LotTestService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
