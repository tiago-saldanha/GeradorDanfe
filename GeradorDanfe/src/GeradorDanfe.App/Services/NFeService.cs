using GeradorDanfe.App.Interfaces;
using NFe.Danfe.PdfClown;
using NFe.Danfe.PdfClown.Modelo;

namespace GeradorDanfe.App.Services
{
    public class NFeService : INFeService
    {
        /// <summary>
        /// Gera o DANFE em formato PDF a partir do XML de uma NFC-e (modelo 65).
        /// </summary>
        /// <param name="xml">
        /// Conteúdo do XML da NFC-e.
        /// </param>
        /// <returns>
        /// Um array de bytes representando o arquivo PDF gerado.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Lançada quando o XML é nulo, vazio ou inválido.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Lançada quando ocorre erro durante a geração do PDF.
        /// </exception>
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
