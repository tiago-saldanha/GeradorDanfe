namespace GeradorDanfe.App.Interfaces
{
    public interface INFeService
    {
        Task<byte[]> GenerateAsync(string xml);
    }
}
