using GeradorDanfe.Application.Models;

namespace GeradorDanfe.Application.Interfaces
{
    public interface IDanfeService
    {
        Task<DanfeResult> GenerateDanfeAsync(Stream stream);
    }
}
