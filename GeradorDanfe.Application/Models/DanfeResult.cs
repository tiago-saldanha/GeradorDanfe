namespace GeradorDanfe.Application.Models
{
    public record DanfeResult(byte[] Bytes, string Name, string ContentType = "application/pdf");
}
