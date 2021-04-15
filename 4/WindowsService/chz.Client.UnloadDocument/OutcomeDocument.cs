using chz.WindowsServices.MDLPClient;
using chz.WindowsServices.UnloadDocument.MDLPClient;
using System.Text.Json.Serialization;

namespace chz.Client.UnloadDocument
{
    /// <summary>
    /// Представляет исходящий документ
    /// Описание api в пункте 4.18 документации
    /// </summary>
    public class OutcomeDocument : Document, IOutcomeDocument
    {
        /// <summary>
        /// Уникальный идентификатор регистратора событий
        /// </summary>
        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        /// <summary>
        /// Уникальный идентификатор системы сформировавшей сообщение
        /// </summary>
        [JsonPropertyName("skzkm_origin_msg_id")]
        public string SkzkmOriginMsgId { get; set; }

        /// <summary>
        /// Идентификатор отчета СУЗ
        /// </summary>
        [JsonPropertyName("skzkm_report_id")]
        public string SkzkmReportId { get; set; }
    }
}
