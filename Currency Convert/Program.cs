using Currency_Convert.Components;
using Currency_Convert.Service;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;
using Currency_Convert.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IConvertCurrence, ConvertCurrence>();
builder.Services.AddHttpClient<IWeatherCast, WeatherCast>();
builder.Services.AddScoped<IDataFormat, DataFormat>();

//builder.Services.AddScoped<IWeatherCast, WeatherCast>();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
  );


// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var app = builder.Build();
// Register your app services before calling Build()builder.Services.AddScoped<IConvertCurrence, ConvertCurrence>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
