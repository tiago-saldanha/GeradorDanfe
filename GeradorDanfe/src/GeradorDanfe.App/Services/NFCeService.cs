using GeradorDanfe.App.Interfaces;
using NFe.Danfe.QuestPdf.ImpressaoNfce;
using QuestPDF.Fluent;

namespace GeradorDanfe.App.Services
{
    public class NFCeService : INFCeService
    {
        /// <summary>
        /// Gera o DANFE em formato PDF a partir do XML de uma NFC-e (modelo 65),
        /// utilizando layout de impressão térmica (80mm).
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
            var danfe = new DanfeNfceDocument(xml, null);
            danfe.TamanhoImpressao(TamanhoImpressao.Impressao80);

            return danfe.GeneratePdf();
        }
    }
}
