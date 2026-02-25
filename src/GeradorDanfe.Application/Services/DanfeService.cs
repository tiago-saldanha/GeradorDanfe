using System.Text;
using GeradorDanfe.Application.Models;
using Microsoft.Extensions.Logging;
using GeradorDanfe.Application.Interfaces;

namespace GeradorDanfe.Application.Services
{
    public class DanfeService(ILogger<DanfeService> logger, IPDFService pdfService) : IDanfeService
    {
        /// <summary>
        /// Processa um XML de NF-e a partir de um <see cref="Stream"/>,
        /// gera o DANFE em formato HTML e converte para PDF.
        /// </summary>
        /// <param name="stream">
        /// Stream contendo o XML da NF-e.
        /// </param>
        /// <returns>
        /// Um <see cref="DanfeResult"/> com o conteúdo do PDF gerado
        /// e o nome do arquivo baseado na chave de acesso da NF-e.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Lançada quando o XML é inválido ou não pode ser processado.
        /// </exception>
        public async Task<DanfeResult> GenerateDanfeAsync(Stream stream)
        {
            var nfe = new NFeResult(stream);

            logger.LogInformation("Reading XML file: {key}", nfe.Key);

            var html = await nfe.GetHtmlDocumentAsync();

            var bytes = pdfService.Generate(html);

            logger.LogInformation("PDF generate succesfully: {key}", nfe.Key);

            return new DanfeResult(bytes, $"{nfe.Key}.pdf");
        }
    }
}
