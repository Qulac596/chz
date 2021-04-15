using System.Text.Json.Serialization;

namespace chz.WindowsServices.ThisLoadDocument.MDLPClient
{
    /// <summary>
    /// Список входящих документов
    /// Описание api в пункте 5.8 документации
    /// </summary>
    public class IncomeDocumentList : IIncomeDocumentList
    {
        /// <summary>
        /// Список
        /// </summary>
        [JsonPropertyName("documents")]
        public IncomeDocument[] Documents { get; set; }

        /// <summary>
        /// Общее количество записей по запросу
        /// </summary>
        [JsonPropertyName("total")]
        public int Count { get; set; }
    }
}
