using System.Text.Json.Serialization;

namespace chz.WindowsServices.MDLPClient
{
    /*
     * Аутентифицированные данные пользователя
     */
    public class AuthenticatedUserInfo
    {
        public AuthenticatedUserInfo(string code, string signature, string password)
        {
            Code = code;
            Signature = signature;
            Password = password;
        }

        [JsonPropertyName("code")]
        public string Code { get; private set; }

        [JsonPropertyName("signature")]
        public string Signature { get; private set; }

        [JsonPropertyName("password")]
        public string Password { get; private set; }
    }
}
