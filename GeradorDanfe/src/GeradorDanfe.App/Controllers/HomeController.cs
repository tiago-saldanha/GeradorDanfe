using System.Diagnostics;
using GeradorDanfe.App.Interfaces;
using GeradorDanfe.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDanfe.App.Controllers
{
    public class HomeController(IGeneratorService service) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> GerarDanfe(IFormFile file)
        {
            try
            {
                var bytes = await service.ExecuteAsync(file);
                return File(bytes, "application/pdf", "danfe.pdf");
            }
            catch (NotSupportedException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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
