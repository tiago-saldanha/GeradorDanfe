namespace GeradorDanfe.App.Interfaces
{
    public interface IPDFService
    {
        byte[] Generate(string html);
    }
}
