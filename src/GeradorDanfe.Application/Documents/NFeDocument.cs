using System.Text;
using NFe.Classes;

namespace GeradorDanfe.Application.Documents
{
    /// <summary>
    /// Representa um documento de NF-e processado a partir de um XML.
    /// </summary>
    /// <remarks>
    /// Esta classe é responsável por carregar e desserializar o conteúdo
    /// de um <see cref="Stream"/> contendo o XML da NF-e para a estrutura
    /// <see cref="nfeProc"/>, fornecida pela biblioteca da SEFAZ.
    /// </remarks>
    public class NFeDocument
    {
        /// <summary>
        /// Estrutura completa da NF-e desserializada a partir do XML.
        /// </summary>
        public nfeProc NfeProc { get; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="NFeDocument"/> 
        /// a partir de um stream contendo o XML da NF-e.
        /// </summary>
        /// <param name="stream">
        /// Stream contendo o XML da NF-e no formato autorizado pela SEFAZ.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Lançada quando o <paramref name="stream"/> for nulo.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Pode ser lançada caso o XML esteja inválido ou não possa ser desserializado.
        /// </exception>
        public NFeDocument(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);

            using var reader = new StreamReader(stream, Encoding.UTF8);
            NfeProc = new nfeProc().CarregardeStream(reader);
        }
    }
}