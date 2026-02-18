using System.Diagnostics;
using System.Text;
using GeradorDanfe.App.Interfaces;
using GeradorDanfe.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDanfe.App.Controllers
{
    public class HomeController(
        ILogger<HomeController> logger,
        INFeGeneratorService nfeGeneratorService,
        INFCeGeneratorService nfceGeneratorService)
        : Controller
    {
        [HttpPost]
        public async Task<IActionResult> GerarDanfe(string documentType, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Arquivo inválido");

            byte[] bytes;
            logger.LogInformation("Lendo o arquivo XML");
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            var xml = Encoding.UTF8.GetString(stream.ToArray());

            logger.LogInformation("Gerando o PDF de NF-e");

            if (documentType == "NFe")
            {
                bytes = nfeGeneratorService.Generate(xml);
            }
            else if (documentType == "NFCe")
            {
                bytes = nfceGeneratorService.Generate(xml);
            }
            else
            {
                return BadRequest("Tipo inválido.");
            }

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
