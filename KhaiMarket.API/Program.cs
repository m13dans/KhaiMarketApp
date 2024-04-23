using KhaiMarket.API.Extension;
using KhaiMarket.API.Products.Infrastructure.Data;
using KhaiMarket.API.Products.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using static KhaiMarket.API.Products.GetProducts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDbConnection"));
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"));
});

builder.Services.AddTransient<GetProductsQuery>();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.Run();
