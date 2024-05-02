using System.Text.Json.Serialization;
using Asp.Versioning;
using KhaiMarket.API.Features.Products;
using KhaiMarket.API.Helpers;
using KhaiMarket.API.Infrastructure.Data;
using KhaiMarket.API.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(option => option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(
    options =>
    {
        // add a custom operation filter which sets default values
        options.OperationFilter<SwaggerDefaultValues>();

        // var fileName = typeof(Program).Assembly.GetName().Name + ".xml";
        // var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

        // // integrate xml comments
        // options.IncludeXmlComments(filePath);
    });

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDbConnection"));
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"));
});

builder.Services.AddProductServices();

// builder.Services.AddMediator();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddMvc()
.AddApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
       {
           var descriptions = app.DescribeApiVersions();

           // build a swagger endpoint for each discovered API version
           foreach (var description in descriptions)
           {
               var url = $"/swagger/{description.GroupName}/swagger.json";
               var name = description.GroupName.ToUpperInvariant();
               options.SwaggerEndpoint(url, name);
           }
       });
}

app.UseHttpsRedirection();

// app.UseExceptionHandler("/error");

//ApiVersionSet apiVersionSet = app.NewApiVersionSet()
//    .HasApiVersion(new ApiVersion(1))
//    .ReportApiVersions()
//    .Build();

//RouteGroupBuilder versionedGroup = app
//    .MapGroup("api/v{apiVersion:apiVersion}")
//    .WithApiVersionSet(apiVersionSet);

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.ApplyMigrations();

app.Run();
