using GeradorDanfe.Application.Models;

namespace GeradorDanfe.Application.Interfaces
{
    /// <summary>
    /// Define o contrato para geração do DANFE em formato PDF
    /// a partir de um XML de Nota Fiscal Eletrônica (NF-e).
    /// </summary>
    /// <remarks>
    /// Implementações desta interface são responsáveis por processar
    /// o XML autorizado da NF-e, gerar o DANFE e retornar o arquivo
    /// resultante encapsulado em um <see cref="DanfeGenerationResult"/>.
    /// </remarks>
    public interface IDanfeService
    {
        /// <summary>
        /// Gera o DANFE em formato PDF a partir de um XML de NF-e.
        /// </summary>
        /// <param name="stream">
        /// Stream contendo o XML autorizado da NF-e.
        /// </param>
        /// <returns>
        /// Uma <see cref="DanfeGenerationResult"/> contendo
        /// os bytes do PDF gerado e o nome do arquivo.
        /// </returns>
        Task<DanfeGenerationResult> GenerateDanfeAsync(Stream stream);
    }
}