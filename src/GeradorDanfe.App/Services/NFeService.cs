using GeradorDanfe.App.Interfaces;
using DFe.Classes.Flags;
using NFe.Classes;
using NFe.Danfe.Html;
using NFe.Danfe.Html.CrossCutting;
using NFe.Danfe.Html.Dominio;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace GeradorDanfe.App.Services
{
    public class NFeService : INFeService
    {
        private LaunchOptions _launchOptions = default!;
        private PdfOptions _pdfOptions = default!;

        public NFeService()
        {
            SetOptions();
        }

        /// <summary>
        /// Gera o DANFE em formato PDF a partir do XML de uma NF-e (modelo 55),
        /// utilizando renderização em HTML e conversão para PDF via Puppeteer (Chromium).
        /// </summary>
        /// <param name="xml">
        /// Conteúdo do XML da NF-e.
        /// </param>
        /// <returns>
        /// Um array de bytes representando o arquivo PDF gerado.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Lançada quando o XML é nulo, vazio ou inválido.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Lançada quando o XML informado não corresponde a uma NF-e (modelo 55).
        /// </exception>
        /// <exception cref="Exception">
        /// Lançada quando ocorre erro durante a geração do HTML ou conversão para PDF.
        /// </exception>
        public async Task<byte[]> GenerateAsync(string xml)
        {
            var danfe = GetDanfeHtml(xml);
            var document = await danfe.ObterDocHtmlAsync();
            return await GeneratePdfAsync(document.Html);
        }

        private async Task<byte[]> GeneratePdfAsync(string html)
        {
            await using var browser = await Puppeteer.LaunchAsync(_launchOptions);

            await using var page = await browser.NewPageAsync();

            await page.SetContentAsync(html);

            return await page.PdfDataAsync(_pdfOptions);
        }

        private DanfeNfeHtml2 GetDanfeHtml(string xml)
        {
            var proc = new nfeProc().CarregarDeXmlString(xml);

            if (proc.NFe.infNFe.ide.mod != ModeloDocumento.NFe) throw new Exception("O XML informado não é um NFe!");

            var danfe = new DanfeNFe(proc.NFe, Status.Autorizada, proc.protNFe.infProt.nProt, "Emissor Fiscal Saldanha - app.cloudtas.com.br");

            return new DanfeNfeHtml2(danfe);
        }

        private void SetOptions()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                _launchOptions = new LaunchOptions
                {
                    Headless = true,
                    Args = ["--no-sandbox"]
                };
            }
            else
            {
                _launchOptions = new LaunchOptions
                {
                    Headless = true,
                    ExecutablePath = "/usr/bin/chromium-browser",
                    Args = new[]
                    {
                        "--no-sandbox",
                        "--disable-setuid-sandbox",
                        "--disable-dev-shm-usage",
                        "--disable-gpu"
                    }
                };
            }

            _pdfOptions = new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true
            };
        }
    }
}
