using System.Text.Json.Serialization;
using Asp.Versioning;
using KhaiMarket.API.Features.Account;
using KhaiMarket.API.Features.Categories;
using KhaiMarket.API.Features.Products;
using KhaiMarket.API.Helpers;
using KhaiMarket.API.Infrastructure.Data;
using KhaiMarket.API.Infrastructure.Data.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(option => option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAntiforgery();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
    {
        // add a custom operation filter which sets default values
        options.OperationFilter<SecurityRequirementsOperationFilter>();
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddTransient<RolesService>();
// Feature Service for CRUD functionality
builder.Services.AddProductServices();
builder.Services.AddCategoryServices();
builder.Services.AddProductBrandServices();

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

// app.UseHttpsRedirection();

// app.UseExceptionHandler("/error");


app.UseCors(o =>
{
    o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});
app.UseAuthorization();

app.MapGroup("api/v2/Account").MapIdentityApi<IdentityUser>();

app.UseStaticFiles();

app.MapControllers();

app.ApplyMigrations();

await app.CreateRoles();

app.Run();
