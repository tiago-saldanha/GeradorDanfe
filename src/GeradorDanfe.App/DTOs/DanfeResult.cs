namespace GeradorDanfe.App.DTOs
{
    public record DanfeResult(byte[] Bytes, string Name, string ContentType = "application/pdf");
}
