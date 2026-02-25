using GeradorDanfe.Application.Enums;
using NFe.Classes;
using NFe.Danfe.Html;
using NFe.Danfe.Html.CrossCutting;
using NFe.Danfe.Html.Dominio;

namespace GeradorDanfe.Application.Generators
{
    /// <summary>
    /// Responsável pela geração do DANFE em formato HTML
    /// a partir de uma NF-e processada.
    /// </summary>
    /// <remarks>
    /// Esta classe encapsula a integração com a biblioteca
    /// de geração de DANFE HTML, isolando dependências externas
    /// da lógica principal da aplicação.
    /// </remarks>
    public class DanfeHtmlGenerator
    {
        private const string DefaultLabel = "GetDanfe - getdanfe.cloudtas.com.br";

        /// <summary>
        /// Gera o documento HTML completo do DANFE correspondente
        /// à NF-e informada.
        /// </summary>
        /// <param name="nfeProc">
        /// Estrutura da NF-e já desserializada.
        /// </param>
        /// <param name="status">
        /// Status da NF-e convertido para o enum interno da aplicação.
        /// </param>
        /// <param name="protocol">
        /// Número do protocolo de autorização da NF-e.
        /// </param>
        /// <returns>
        /// Uma <see cref="Task{TResult}"/> contendo o HTML completo do DANFE.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Lançada quando <paramref name="nfeProc"/> for nulo.
        /// </exception>
        public static async Task<string> GenerateAsync(
            nfeProc nfeProc,
            StatusNFe status,
            string protocol)
        {
            ArgumentNullException.ThrowIfNull(nfeProc);

            var danfe = new DanfeNFe(
                nfeProc.NFe,
                (Status)status,
                protocol,
                DefaultLabel);

            var htmlGenerator = new DanfeNfeHtml2(danfe);

            var document = await htmlGenerator.ObterDocHtmlAsync();

            return document.Html;
        }
    }
}