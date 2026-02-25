using System.Text;
using NFe.Classes;
using NFe.Danfe.Html;
using NFe.Danfe.Html.CrossCutting;
using NFe.Danfe.Html.Dominio;

namespace GeradorDanfe.Application.Models
{
    /// <summary>
    /// Representa o resultado do processamento de um XML de NF-e,
    /// encapsulando a estrutura da nota, a geração do DANFE em HTML
    /// e informações relevantes como chave de acesso e protocolo.
    /// </summary>
    public class NFeResult
    {
        /// <summary>
        /// Estrutura completa da NF-e processada a partir do XML.
        /// </summary>
        public nfeProc NfeProc { get; init; }

        private DanfeNFe DanfeNFe { get; init; }
        private DanfeNfeHtml2 DanfeNfeHtml { get; init; }

        private string Label { get; init; } = "GetDanfe - getdanfe.cloudtas.com.br";

        /// <summary>
        /// Inicializa uma nova instância de <see cref="NFeResult"/> 
        /// a partir de um <see cref="Stream"/> contendo o XML da NF-e.
        /// </summary>
        /// <param name="stream">
        /// Stream contendo o XML da NF-e.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Lançada quando o stream informado for nulo.
        /// </exception>
        public NFeResult(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            NfeProc = new nfeProc().CarregardeStream(reader);
            DanfeNFe = new DanfeNFe(NfeProc.NFe, Status, Protocol, Label);
            DanfeNfeHtml = new DanfeNfeHtml2(DanfeNFe);
        }

        /// <summary>
        /// Gera o documento HTML completo do DANFE correspondente à NF-e processada.
        /// </summary>
        /// <returns>
        /// Uma string contendo o HTML completo do DANFE.
        /// </returns>
        public async Task<string> GetHtmlDocumentAsync()
        {
            var document = await DanfeNfeHtml.ObterDocHtmlAsync();
            return document.Html;
        }

        /// <summary>
        /// Obtém a chave de acesso da NF-e removendo o prefixo "NFe".
        /// </summary>
        public string Key
            => NfeProc.NFe.infNFe.Id.Replace("NFe", string.Empty);

        private string Protocol
            => NfeProc.protNFe.infProt.nProt;

        private Status Status
        {
            get
            {
                var status = NfeProc.protNFe.infProt.cStat;

                return status switch
                {
                    100 => Status.Autorizada,
                    101 => Status.Cancelada,
                    110 => Status.Negada,
                    _ => Status.NaoAuotorizada
                };
            }
        }
    }
}