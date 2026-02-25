namespace GeradorDanfe.Application.Enums
{
    /// <summary>
    /// Representa os possíveis status de uma Nota Fiscal Eletrônica (NF-e)
    /// após o processamento e retorno da SEFAZ.
    /// </summary>
    /// <remarks>
    /// Os valores deste enum são utilizados para mapear o código de status
    /// (<c>cStat</c>) retornado no protocolo de autorização da NF-e,
    /// convertendo-o para um formato mais expressivo e semântico
    /// dentro da aplicação.
    /// </remarks>
    public enum StatusNFe
    {
        /// <summary>
        /// Indica que a NF-e foi autorizada pela SEFAZ
        /// (código de status 100).
        /// </summary>
        Autorizada,

        /// <summary>
        /// Indica que a NF-e foi cancelada
        /// (código de status 101).
        /// </summary>
        Cancelada,

        /// <summary>
        /// Indica que a NF-e não foi autorizada pela SEFAZ
        /// (qualquer código diferente dos previstos explicitamente).
        /// </summary>
        NaoAutorizada,

        /// <summary>
        /// Indica que o uso da NF-e foi denegado pela SEFAZ
        /// (código de status 110).
        /// </summary>
        UsoDenegado,
    }
}