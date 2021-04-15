using System.Text.Json.Serialization;

namespace chz.WindowsServices.MDLPClient
{
    /*
     * Данные для получения кода аунтификации
     */
    public class AuntificationUserInfo
    {
        public AuntificationUserInfo(string clientId, string clientSecret, string userId, AuthType authType)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            UserId = userId;
            AuthType = authType;
        }

        [JsonPropertyName("client_id")]
        public string ClientId { get; private set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; private set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; private set; }

        [JsonPropertyName("auth_type")]
        public AuthType AuthType { get; private set; }
    }
}
