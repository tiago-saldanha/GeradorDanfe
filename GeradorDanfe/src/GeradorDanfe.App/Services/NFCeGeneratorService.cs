using GeradorDanfe.App.Interfaces;
using NFe.Danfe.QuestPdf.ImpressaoNfce;
using QuestPDF.Fluent;

namespace GeradorDanfe.App.Services
{
    public class NFCeGeneratorService : INFCeGeneratorService
    {
        /// <summary>
        /// Gera uma DANFE a partir de um xml de uma Nota Fiscal do Consumidor Eletrônica (NFC-e)
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public byte[] Generate(string xml)
        {
            try
            {
                var danfe = new DanfeNfceDocument(xml, null);
                danfe.TamanhoImpressao(TamanhoImpressao.Impressao80);

                var bytes = danfe.GeneratePdf();

                return bytes;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar o PDF pelo QuestPDFGeneratorService", ex);
            }
        }
    }
}
