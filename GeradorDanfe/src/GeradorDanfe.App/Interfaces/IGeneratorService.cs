using GeradorDanfe.App.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDanfe.App.Interfaces
{
    public interface IGeneratorService
    {
        Task<DanfeResult> ExecuteAsync(IFormFile file);
    }
}
