using GeradorDanfe.App.Interfaces;
using GeradorDanfe.App.Services;
using PuppeteerSharp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<INFeService, NFeService>();
builder.Services.AddTransient<IGeneratorService, GeneratorService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Warm-up Puppeteer
await new BrowserFetcher().DownloadAsync();

app.Run();
