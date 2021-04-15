using chz.WindowsServices.MDLPClient;
using System.Text.Json.Serialization;

namespace chz.Client
{
    /// <summary>
    /// Токен сессии
    /// </summary>
    internal class SessionToken : ISessionToken
    {
        /// <summary>
        /// Токен
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; }

        /// <summary>
        /// Время жизни
        /// </summary>
        [JsonPropertyName("life_time")]
        public int LifeTime { get; set; }
    }
}
