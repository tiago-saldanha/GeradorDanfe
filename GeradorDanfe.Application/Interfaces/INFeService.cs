namespace GeradorDanfe.Application.Interfaces
{
    public interface INFeService
    {
        Task<byte[]> GenerateAsync(string xml);
    }
}
