using Currency_Convert.Components;
using Microsoft.AspNetCore.Components.Server;
using Currency_Convert.Service;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IConvertCurrence, ConvertCurrence>();
   

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

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
