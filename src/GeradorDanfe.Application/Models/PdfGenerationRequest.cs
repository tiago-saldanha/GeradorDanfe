namespace GeradorDanfe.Application.Models
{
    public sealed record PdfGenerationRequest(
        string Html,
        string DocumentTitle = null,
        bool Landscape = false
    );
}
