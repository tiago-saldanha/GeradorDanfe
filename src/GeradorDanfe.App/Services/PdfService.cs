using DinkToPdf;
using DinkToPdf.Contracts;
using GeradorDanfe.App.Interfaces;

namespace GeradorDanfe.App.Services
{
    public class PDFService(IConverter converter) : IPDFService
    {
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
