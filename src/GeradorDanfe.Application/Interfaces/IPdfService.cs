using GeradorDanfe.Application.Models;

namespace GeradorDanfe.Application.Interfaces
{
    /// <summary>
    /// Define o contrato para conversão de conteúdo HTML em PDF.
    /// </summary>
    public interface IPdfService
    {
        /// <summary>
        /// Gera um arquivo PDF a partir de um conteúdo HTML.
        /// </summary>
        /// <param name="request">
        /// Parâmetros necessários para geração do PDF.
        /// </param>
        /// <returns>
        /// Um array de bytes representando o PDF gerado.
        /// </returns>
        byte[] Generate(PdfGenerationRequest request);
    }
}