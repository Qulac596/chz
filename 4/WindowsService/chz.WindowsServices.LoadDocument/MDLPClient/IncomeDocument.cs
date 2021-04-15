using chz.WindowsServices.MDLPClient;
using System.Text.Json.Serialization;

namespace chz.WindowsServices.LoadDocument.MDLPClient
{
    /// <summary>
    /// Представляет входящий документ
    /// Описание в пункте 4.19 документации
    /// </summary>
    public class IncomeDocument : Document, IIncomeDocument
    {
        /// <summary>
        /// Идентификатор отправителя документа в «ИС "Маркировка". МДЛП»
        /// </summary>
        [JsonPropertyName("sender_sys_id")]
        public string SenderSysId { get; set; }
    }
}
