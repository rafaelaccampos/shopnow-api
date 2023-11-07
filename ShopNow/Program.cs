using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using ShopNow.Domain.Factory;
using ShopNow.Domain.Repositories;
using ShopNow.Infra.Data;
using ShopNow.Infra.Data.Dao;
using ShopNow.Infra.Data.Factories;
using ShopNow.Infra.Data.Queries;
using ShopNow.Infra.Data.Repositories.Database;
using ShopNow.Infra.Migrations;
using ShopNow.UseCases;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddDbContext<ShopContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Shops")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb =>
    rb.AddSqlServer()
    .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Shops"))
    .ScanIn(typeof(Migrations).Assembly).For.Migrations());

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IOrderDAO, OrderDAO>();
builder.Services.AddScoped<IAbstractRepositoryFactory, DatabaseRepositoryFactory>();
builder.Services.AddScoped<PlaceOrder>();
builder.Services.AddScoped<SimulateFreight>();
builder.Services.AddScoped<ValidateCoupon>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program 
{ 
}
