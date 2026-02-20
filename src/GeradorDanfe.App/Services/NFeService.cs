using GeradorDanfe.App.Interfaces;
using DFe.Classes.Flags;
using NFe.Classes;
using NFe.Danfe.Html;
using NFe.Danfe.Html.CrossCutting;
using NFe.Danfe.Html.Dominio;

namespace GeradorDanfe.App.Services
{
    public class NFeService(IPDFService pdfService) : INFeService
    {
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
            var result = pdfService.Generate(document.Html);
            return result;
        }

        private DanfeNfeHtml2 GetDanfeHtml(string xml)
        {
            var proc = new nfeProc().CarregarDeXmlString(xml);

            if (proc.NFe.infNFe.ide.mod != ModeloDocumento.NFe) throw new Exception("O XML informado não é um NFe!");

            var danfe = new DanfeNFe(proc.NFe, Status.Autorizada, proc.protNFe.infProt.nProt, "Emissor Fiscal Saldanha - app.cloudtas.com.br");

            return new DanfeNfeHtml2(danfe);
        }
    }
}
