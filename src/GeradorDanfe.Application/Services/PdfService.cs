using DinkToPdf;
using DinkToPdf.Contracts;
using GeradorDanfe.Application.Interfaces;
using GeradorDanfe.Application.Models;

namespace GeradorDanfe.Application.Services
{
    /// <summary>
    /// Serviço responsável por gerar arquivos PDF a partir de conteúdo HTML,
    /// utilizando a biblioteca DinkToPdf.
    /// </summary>
    /// <remarks>
    /// Esta implementação encapsula a integração com o mecanismo de conversão
    /// HTML para PDF, isolando a dependência externa da camada de aplicação.
    /// </remarks>
    public class PdfService(IConverter converter) : IPdfService
    {
        /// <summary>
        /// Gera um documento PDF em formato de array de bytes
        /// a partir dos parâmetros informados na requisição.
        /// </summary>
        /// <param name="request">
        /// Objeto contendo os dados necessários para geração do PDF,
        /// incluindo o conteúdo HTML e configurações opcionais.
        /// </param>
        /// <returns>
        /// Um array de bytes representando o arquivo PDF gerado.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Lançada quando <paramref name="request"/> for nulo.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Lançada quando o conteúdo HTML não for informado.
        /// </exception>
        public byte[] Generate(PdfGenerationRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentException.ThrowIfNullOrEmpty(request.Html);

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
                        HtmlContent = request.Html,
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
