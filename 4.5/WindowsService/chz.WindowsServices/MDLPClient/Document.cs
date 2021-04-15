using chz.Common;
using System;
using System.Text.Json.Serialization;

namespace chz.WindowsServices.MDLPClient
{
    /// <summary>
    /// Представляет документ
    /// Описание в пункте 4.17 документации
    /// </summary>
    public class Document : IDocument
    {
        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// Уникальный идентификатор документа
        /// </summary>
        [JsonPropertyName("document_id")]
        public string DocumentId { get; set; }

        /// <summary>
        /// Дата получения
        /// </summary>
        [JsonPropertyName("date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Дата обработки документа
        /// </summary>
        [JsonPropertyName("processed_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ProcessedDate { get; set; }

        /// <summary>
        /// Отправитель документа
        /// </summary>
        [JsonPropertyName("sender")]
        public string Sender { get; set; }

        /// <summary>
        /// Получатель документа
        /// </summary>
        [JsonPropertyName("receiver")]
        public string Receiver { get; set; }

        /// <summary>
        /// Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [JsonPropertyName("sys_id")]
        public string SysId { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        [JsonPropertyName("doc_type")]
        public int? DocType { get; set; }

        /// <summary>
        /// Статус документа
        /// </summary>
        [JsonPropertyName("doc_status")]
        public MDLPDocumentStatus Status { get; set; }

        /// <summary>
        /// Тип загрузки в систему
        /// </summary>
        [JsonPropertyName("file_uploadtype")]
        public int? FileUploadtype { get; set; }

        /// <summary>
        /// Версия документа
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
