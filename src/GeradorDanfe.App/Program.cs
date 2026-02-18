using GeradorDanfe.App.Interfaces;
using GeradorDanfe.App.Services;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<INFeService, NFeService>();
builder.Services.AddTransient<INFCeService, NFCeService>();
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

QuestPDF.Settings.License = LicenseType.Community;

app.Run();
