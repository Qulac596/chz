using System;
using System.Text.Json.Serialization;

namespace chz.WindowsServices.LoadDocument.MDLPClient
{
    /// <summary>
    /// Фильтр документов.
    /// Описание api в пункте 4.20 документации
    /// </summary>
    public class DocFilter
    {
        /// <summary>
        /// Дата начала периода фильтрации
        /// </summary>
        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода фильтрации
        /// </summary>
        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Уникальный идентификатор документа
        /// </summary>
        [JsonPropertyName("document_id")]
        public string DocumentId { get; set; }

        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        [JsonPropertyName("doc_type")]
        public int? DocType { get; set; }

        /// <summary>
        /// Статус документа
        /// </summary>
        [JsonPropertyName("doc_status")]
        public string DocStatus { get; set; }

        /// <summary>
        /// Тип загрузки в систему
        /// </summary>
        [JsonPropertyName("file_uploadtype")]
        public int? FileUploadtype { get; set; }

        /// <summary>
        /// Дата обработки документа: начало периода
        /// </summary>
        [JsonPropertyName("processed_date_from")]
        public DateTime? ProcessedDateFrom { get; set; }

        /// <summary>
        /// Дата обработки документа: окончание периода
        /// </summary>
        [JsonPropertyName("processed_date_to")]
        public DateTime? ProcessedDateTo { get; set; }

        /// <summary>
        /// Уникальный идентификатор отправителя
        /// </summary>
        [JsonPropertyName("sender_id")]
        public string SenderId { get; set; }

        /// <summary>
        /// Уникальный идентификатор получателя
        /// </summary>
        [JsonPropertyName("receiver_id")]
        public string ReceiverId { get; set; }

        /// <summary>
        /// Идентификатор отчета СУЗ
        /// </summary>
        [JsonPropertyName("skzkm_report_id")]
        public string SkzkmReportId { get; set; }
    }
}
