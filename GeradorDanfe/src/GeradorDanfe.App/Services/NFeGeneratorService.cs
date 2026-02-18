using GeradorDanfe.App.Interfaces;
using NFe.Danfe.PdfClown;
using NFe.Danfe.PdfClown.Modelo;

namespace GeradorDanfe.App.Services
{
    public class NFeGeneratorService : INFeGeneratorService
    {
        /// <summary>
        /// Gera uma DANFE a partir de um xml de uma Nota Fiscal Eletrônica (NFC-e)
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>Retorna um array de bytes contendo o PDF.</returns>
        public byte[] Generate(string xml)
        {
            var model = DanfeViewModelCreator.CriarDeStringXml(xml);

            using var stream = new MemoryStream();
            using var danfe = new DanfeDoc(model);

            danfe.Gerar();

            return danfe.ObterPdfBytes(stream);
        }
    }
}
