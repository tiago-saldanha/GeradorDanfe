namespace GeradorDanfe.Application.Models
{
    /// <summary>
    /// Representa o resultado da geração do DANFE,
    /// contendo os dados binários do arquivo, o nome
    /// e o tipo de conteúdo (MIME type).
    /// </summary>
    /// <param name="Bytes">
    /// Conteúdo do arquivo gerado em formato de array de bytes.
    /// </param>
    /// <param name="Name">
    /// Nome do arquivo que será utilizado no download.
    /// </param>
    /// <param name="ContentType">
    /// Tipo de conteúdo (MIME type) do arquivo.
    /// O valor padrão é "application/pdf".
    /// </param>
    public record DanfeResult(byte[] Bytes, string Name, string ContentType = "application/pdf");
}
