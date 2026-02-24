using System.Text;
using System.Xml.Linq;
using GeradorDanfe.Application.Models;
using Microsoft.Extensions.Logging;
using GeradorDanfe.Application.Interfaces;

namespace GeradorDanfe.Application.Services
{
    public class GeneratorService(
        ILogger<GeneratorService> logger, 
        INFeService nfeService)
        : IGeneratorService
    {
        /// <summary>
        /// Lê um arquivo XML de NF-e (modelo 55),
        /// identifica automaticamente o tipo do documento e gera o DANFE em PDF.
        /// </summary>
        /// <param name="file">
        /// Arquivo XML contendo a NF-e.
        /// </param>
        /// <returns>
        /// Um <see cref="DanfeResult"/> contendo o PDF gerado e o nome do arquivo,
        /// que utiliza a chave de acesso do documento.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Lançada quando o arquivo é inválido, vazio ou quando o modelo do documento
        /// não é suportado.
        /// </exception>
        public async Task<DanfeResult> ExecuteAsync(Stream stream)
        {
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var xml = await reader.ReadToEndAsync();
            var key = GetDocumentKey(xml);
            
            logger.LogInformation("Lendo o arquivo XML: {key}", key);

            var bytes = await GetPdfBytesAsync(xml);
            
            logger.LogInformation("PDF gerado com sucesso: {key}.pdf", key);

            return new DanfeResult(bytes, $"{key}.pdf");
        }

        private async Task<byte[]> GetPdfBytesAsync(string xml)
        {
            byte[] bytes;
            
            if (xml.Contains("<mod>55</mod>"))
            {
                bytes = await nfeService.GenerateAsync(xml);
            }
            else
            {
                throw new NotSupportedException("Informe um XML de uma NF-e");
            }
            
            return bytes;
        }

        private string GetDocumentKey(string xml)
        {
            var infNFe = XDocument
                .Parse(xml)
                .Descendants()
                .FirstOrDefault(e => e.Name.LocalName == "infNFe") ?? throw new Exception("Não foi possível ler a chave de acesso no XML fornecido.");

            var id = infNFe.Attribute("Id")?.Value ?? throw new Exception("Chave de acesso inválida presente no XML é inválida.");
            
            return id.Replace("NFe", "");
        }
    }
}
