namespace GeradorDanfe.Application.Interfaces
{
    public interface IPDFService
    {
        byte[] Generate(string html);
    }
}
