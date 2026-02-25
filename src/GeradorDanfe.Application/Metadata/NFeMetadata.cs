using GeradorDanfe.Application.Enums;
using NFe.Classes;

namespace GeradorDanfe.Application.Metadata
{
    /// <summary>
    /// Fornece informações derivadas de uma NF-e processada,
    /// como chave de acesso, protocolo e status de autorização.
    /// </summary>
    /// <remarks>
    /// Esta classe encapsula regras de interpretação dos dados contidos
    /// na estrutura <see cref="nfeProc"/>, isolando a lógica de negócio
    /// relacionada ao status e metadados da NF-e.
    /// </remarks>
    public class NFeMetadata
    {
        private readonly nfeProc _nfeProc;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="NFeMetadata"/>.
        /// </summary>
        /// <param name="nfeProc">
        /// Estrutura da NF-e já desserializada a partir do XML autorizado.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Lançada quando <paramref name="nfeProc"/> for nulo.
        /// </exception>
        public NFeMetadata(nfeProc nfeProc)
        {
            ArgumentNullException.ThrowIfNull(nameof(nfeProc));

            _nfeProc = nfeProc;
        }

        /// <summary>
        /// Obtém a chave de acesso da NF-e.
        /// </summary>
        /// <remarks>
        /// Remove automaticamente o prefixo "NFe" presente no identificador.
        /// </remarks>
        public string Key =>
            _nfeProc.NFe.infNFe.Id.Replace("NFe", string.Empty);

        /// <summary>
        /// Obtém o número do protocolo de autorização da NF-e.
        /// </summary>
        public string Protocol =>
            _nfeProc.protNFe.infProt.nProt;

        /// <summary>
        /// Obtém o status da NF-e convertido para o enum interno da aplicação.
        /// </summary>
        /// <remarks>
        /// Realiza o mapeamento do código de status (<c>cStat</c>) retornado
        /// pela SEFAZ para o enum <see cref="StatusNFe"/>.
        /// </remarks>
        public StatusNFe Status
        {
            get
            {
                var status = _nfeProc.protNFe.infProt.cStat;

                return status switch
                {
                    100 => StatusNFe.Autorizada,
                    101 => StatusNFe.Cancelada,
                    110 => StatusNFe.UsoDenegado,
                    _ => StatusNFe.NaoAutorizada
                };
            }
        }
    }
}