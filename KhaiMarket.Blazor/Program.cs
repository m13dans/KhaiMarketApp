using KhaiMarket.Blazor.Client;
using KhaiMarket.Blazor.Components;
using KhaiMarket.Blazor.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<ProductStateContainer>();

var productsApiUrl = builder.Configuration["ProductsApiUrl"] ??
throw new Exception("GameStoreApiUrl is not set");

builder.Services.AddHttpClient<ProductClient>(
    client => client.BaseAddress = new Uri(productsApiUrl));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
