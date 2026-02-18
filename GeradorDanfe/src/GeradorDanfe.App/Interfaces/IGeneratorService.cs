using Microsoft.AspNetCore.Mvc;

namespace GeradorDanfe.App.Interfaces
{
    public interface IGeneratorService
    {
        Task<byte[]> ExecuteAsync(IFormFile file);
    }
}
