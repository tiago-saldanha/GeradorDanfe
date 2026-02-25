using DinkToPdf;
using DinkToPdf.Contracts;
using GeradorDanfe.Application.Interfaces;

namespace GeradorDanfe.Application.Services
{
    /// <summary>
    /// Serviço responsável por gerar arquivos PDF a partir de conteúdo HTML
    /// utilizando a biblioteca DinkToPdf.
    /// </summary>
    public class PDFService(IConverter converter) : IPDFService
    {
        /// <summary>
        /// Gera um documento PDF em formato de array de bytes a partir de uma string HTML.
        /// </summary>
        /// <param name="html">
        /// Conteúdo HTML que será convertido em PDF.
        /// Deve estar completo e devidamente formatado.
        /// </param>
        /// <returns>
        /// Um array de bytes representando o arquivo PDF gerado.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Lançada quando o conteúdo HTML informado for nulo.
        /// </exception>
        public byte[] Generate(string html)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    Margins = new MarginSettings()
                    {
                        Top = 2,
                        Bottom = 2,
                        Left = 2,
                        Right = 2
                    }
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        LoadSettings = new LoadSettings
                        {
                            ZoomFactor = 1.3
                        }
                    }
                }
            };

            return converter.Convert(doc);
        }
    }
}
