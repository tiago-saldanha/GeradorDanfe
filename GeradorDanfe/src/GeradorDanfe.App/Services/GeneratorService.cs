using System.Text;
using GeradorDanfe.App.Interfaces;

namespace GeradorDanfe.App.Services
{
    public class GeneratorService(
        ILogger<GeneratorService> logger, 
        INFeGeneratorService nfeGeneratorService, 
        INFCeGeneratorService nfceGeneratorService)
        : IGeneratorService
    {
        public async Task<byte[]> ExecuteAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) throw new NotSupportedException("Selecione um arquivo XML válido.");
            
            var xml = await ReadFormFileAsync(file);
            
            logger.LogInformation("Gerando o PDF");
            return GetPdfBytes(xml);
        }

        private async Task<string> ReadFormFileAsync(IFormFile file)
        {
            logger.LogInformation("Lendo o arquivo XML");
            
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
                bytes = nfeGeneratorService.Generate(xml);
            }
            else if (xml.Contains("<mod>65</mod>"))
            {
                bytes = nfceGeneratorService.Generate(xml);
            }
            else
            {
                throw new NotSupportedException("Informe um XML de uma NF-e ou NFC-e!");
            }
            
            return bytes;
        }
    }
}
