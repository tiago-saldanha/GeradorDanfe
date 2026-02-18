using System.Text;
using System.Xml.Linq;
using GeradorDanfe.App.DTOs;
using GeradorDanfe.App.Interfaces;

namespace GeradorDanfe.App.Services
{
    public class GeneratorService(
        ILogger<GeneratorService> logger, 
        INFeService nfeService, 
        INFCeService nfceService)
        : IGeneratorService
    {
        /// <summary>
        /// Lê um arquivo XML de NF-e (modelo 55) ou NFC-e (modelo 65),
        /// identifica automaticamente o tipo do documento e gera o DANFE em PDF.
        /// </summary>
        /// <param name="file">
        /// Arquivo XML contendo a NF-e ou NFC-e.
        /// </param>
        /// <returns>
        /// Um <see cref="DanfeResult"/> contendo o PDF gerado e o nome do arquivo,
        /// que utiliza a chave de acesso do documento.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Lançada quando o arquivo é inválido, vazio ou quando o modelo do documento
        /// não é suportado.
        /// </exception>
        public async Task<DanfeResult> ExecuteAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) throw new NotSupportedException("Selecione um arquivo XML válido.");
            
            var xml = await ReadFormFileAsync(file);
            

            var key = GetDocumentKey(xml);
            logger.LogInformation("Lendo o arquivo XML: {key}", key);

            var bytes = GetPdfBytes(xml);
            logger.LogInformation("PDF gerado com sucesso: {key}.pdf", key);

            return new DanfeResult(bytes, $"{key}.pdf");
        }

        private async Task<string> ReadFormFileAsync(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;
            
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        private byte[] GetPdfBytes(string xml)
        {
            byte[] bytes;
            
            if (xml.Contains("<mod>55</mod>"))
            {
                bytes = nfeService.Generate(xml);
            }
            else if (xml.Contains("<mod>65</mod>"))
            {
                bytes = nfceService.Generate(xml);
            }
            else
            {
                throw new NotSupportedException("Informe um XML de uma NF-e ou NFC-e!");
            }
            
            return bytes;
        }

        private string GetDocumentKey(string xml)
        {
            var document = XDocument.Parse(xml);

            var infNFe = document.Descendants()
                    .FirstOrDefault(e => e.Name.LocalName == "infNFe");

            if (infNFe == null)
                throw new Exception("Não foi possível ler a chave de acesso no XML fornecido.");

            var id = infNFe.Attribute("Id")?.Value;

            if (string.IsNullOrEmpty(id) || !id.StartsWith("NFe"))
                throw new Exception("Chave de acesso inválida presente no XML é inválida.");

            return id.Replace("NFe", "");
        }
    }
}
