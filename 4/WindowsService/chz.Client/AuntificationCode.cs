using chz.WindowsServices.MDLPClient;
using System.Text.Json.Serialization;

namespace chz.Client
{
    /// <summary>
    /// Код аунтификации
    /// </summary>
    internal class AuntificationCode : IAuntificationCode
    {
        /// <summary>
        /// Код
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
