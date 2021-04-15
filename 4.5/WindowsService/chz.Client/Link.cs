using chz.WindowsServices.MDLPClient;
using System.Text.Json.Serialization;

namespace chz.Client
{
    /// <summary>
    /// Ссылка для загрузки документа
    /// </summary>
    public class Link : ILink
    {
        /// <summary>
        /// Url
        /// </summary>
        [JsonPropertyName("link")]
        public string Url { get; set; }
    }
}
