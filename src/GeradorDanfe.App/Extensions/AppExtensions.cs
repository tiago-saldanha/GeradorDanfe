using DinkToPdf.Contracts;
using DinkToPdf;
using GeradorDanfe.App.Context;
using GeradorDanfe.App.Interfaces;
using GeradorDanfe.App.Services;
using System.Runtime.InteropServices;

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
            services.AddTransient<IPDFService, PDFService>();
            services.AddTransient<INFeService, NFeService>();
            services.AddTransient<IGeneratorService, GeneratorService>();
            return services;
        }

        public static string WindowsPathDll = Path.Combine(AppContext.BaseDirectory, "Lib", "libwkhtmltox.dll");
        public static string LinuxPathSo = "/usr/local/lib/libwkhtmltox.so";
    }
}
