using DinkToPdf.Contracts;
using DinkToPdf;
using GeradorDanfe.App.Context;
using System.Runtime.InteropServices;
using GeradorDanfe.Application.Services;
using GeradorDanfe.Application.Interfaces;

namespace GeradorDanfe.App.Extensions
{
    public static class AppExtensions
    {
        public static WebApplicationBuilder ConfigureCustomAssembly(this WebApplicationBuilder builder)
        {
            var path = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? WindowsPathDll : LinuxPathSo;

            if (!File.Exists(path)) 
                throw new Exception($"wkhtmltox library not found at {path}");

            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(path);
            return builder;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));
            services.AddControllersWithViews();
            services.AddScoped<IPDFService, PDFService>();
            services.AddScoped<IDanfeService, DanfeService>();
            return services;
        }

        public static string WindowsPathDll = Path.Combine(AppContext.BaseDirectory, "Lib", "libwkhtmltox.dll");
        public static string LinuxPathSo = "/usr/local/lib/libwkhtmltox.so";
    }
}
