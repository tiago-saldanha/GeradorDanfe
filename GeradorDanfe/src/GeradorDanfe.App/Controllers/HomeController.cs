using System.Diagnostics;
using GeradorDanfe.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDanfe.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GerarDanfe(IFormFile fileXml)
        {
            if (fileXml == null || fileXml.Length == 0)
                return BadRequest("Arquivo inválido");

            using var stream = new MemoryStream();
            await fileXml.CopyToAsync(stream);

            stream.Position = 0;

            var bytes = stream.ToArray();

            return File(bytes, "application/pdf", "danfe.pdf");
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
