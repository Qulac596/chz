using System.Text.Json.Serialization;

namespace chz.WindowsServices.MDLPClient
{
    /// <summary>
    /// Типы аунтификации
    /// описание в пункте 4.14 документации
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AuthType
    {
        /// <summary>
        /// Аутентификация с помощью пароля
        /// </summary>
        PASSWORD,

        /// <summary>
        /// Аутентификация с помощью подписанного одноразового кода
        /// </summary>
        SIGNED_CODE
    }
}
