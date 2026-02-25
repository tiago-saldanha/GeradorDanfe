using Microsoft.Extensions.Logging;
using GeradorDanfe.Application.Documents;
using GeradorDanfe.Application.Generators;
using GeradorDanfe.Application.Interfaces;
using GeradorDanfe.Application.Metadata;
using GeradorDanfe.Application.Models;

namespace GeradorDanfe.Application.Services
{
    public class DanfeService(ILogger<DanfeService> logger, IPdfService pdfService) : IDanfeService
    {
        /// <summary>
        /// Processa um XML de NF-e, gera o DANFE em formato PDF
        /// e retorna o resultado contendo o arquivo gerado.
        /// </summary>
        /// <remarks>
        /// Este método realiza a leitura e desserialização do XML,
        /// extrai os metadados da NF-e, gera o DANFE em HTML e
        /// converte o conteúdo para PDF.
        /// </remarks>
        /// <param name="stream">
        /// Stream contendo o XML autorizado da NF-e.
        /// </param>
        /// <returns>
        /// Uma <see cref="DanfeGenerationResult"/> contendo
        /// os bytes do PDF gerado e o nome do arquivo baseado
        /// na chave de acesso da NF-e.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Lançada quando o <paramref name="stream"/> for nulo.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Pode ser lançada caso o XML esteja inválido
        /// ou não possa ser processado.
        /// </exception>
        public async Task<DanfeGenerationResult> GenerateDanfeAsync(Stream stream)
        {
            var document = new NFeDocument(stream);
            var metadata = new NFeMetadata(document.NfeProc);
            logger.LogInformation("Reading XML file: {key}", metadata.Key);

            var html = await DanfeHtmlGenerator.GenerateAsync(document.NfeProc, metadata.Status, metadata.Protocol);
            var request = new PdfGenerationRequest(html);
            var bytes = pdfService.Generate(request);

            logger.LogInformation("PDF generate succesfully: {key}", metadata.Key);
            return new DanfeGenerationResult(bytes, $"{metadata.Key}.pdf");
        }
    }
}
