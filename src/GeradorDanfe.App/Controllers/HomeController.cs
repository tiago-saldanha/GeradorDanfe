using System.Diagnostics;
using GeradorDanfe.App.Interfaces;
using GeradorDanfe.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDanfe.App.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IGeneratorService service) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> GerarDanfe(IFormFile file)
        {
            try
            {
                var danfe = await service.ExecuteAsync(file);
                return File(danfe.Bytes, danfe.ContentType, danfe.Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro inesperado ao gerar DANFE.");
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Index()
        {
            return View();
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
