using GeradorDanfe.Application.Models;

namespace GeradorDanfe.Application.Interfaces
{
    public interface IGeneratorService
    {
        Task<DanfeResult> ExecuteAsync(Stream stream);
    }
}
