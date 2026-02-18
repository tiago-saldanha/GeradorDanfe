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
        public async Task<IActionResult> GerarDanfe(IFormFile file)
            => await ExecuteAsync(file);

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

        private string GetDocumentType(string xml)
        {
            if (xml.Contains("<mod>55</mod>"))
            {
                return "NFe";
            }
            else if (xml.Contains("<mod>65</mod>"))
            {
                return "NFCe";
            }
            else
            {
                throw new NotSupportedException("Informe um XML de uma NF-e ou NFC-e!");
            }
        }

        private async Task<ActionResult> ExecuteAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    TempData["Error"] = "Selecione um arquivo XML válido.";
                    return RedirectToAction("Index");
                }

                logger.LogInformation("Lendo o arquivo XML");

                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0;

                var xml = Encoding.UTF8.GetString(stream.ToArray());

                logger.LogInformation("Gerando o PDF");

                byte[] bytes;

                var documentType = GetDocumentType(xml);

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
                    TempData["Error"] = "Tipo de documento inválido.";
                    return RedirectToAction("Index");
                }

                return File(bytes, "application/pdf", "danfe.pdf");
            }
            catch (NotSupportedException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao gerar o DANFE");
                TempData["Error"] = "Não foi possível gerar o DANFE. Verifique o XML enviado.";
                return RedirectToAction("Index");
            }
        }
    }
}
