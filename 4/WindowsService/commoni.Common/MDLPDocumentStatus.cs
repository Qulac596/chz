
using System.Text.Json.Serialization;

namespace chz.Common
{
    /// <summary>
    /// Статусы документа
    /// Описание в пункте 4.12 документации
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MDLPDocumentStatus
    {
        /// <summary>
        /// Загрузка документа
        /// </summary>
        UPLOADING_DOCUMENT,

        /// <summary>
        /// Первичная обработка документа
        /// </summary>
        PROCESSING_DOCUMENT,

        /// <summary>
        /// Обработка документа системой
        /// </summary>
        CORE_PROCESSING_DOCUMENT,

        /// <summary>
        /// Подготовка ответа
        /// </summary>
        CORE_PROCESSED_DOCUMENT,

        /// <summary>
        /// Документ обработан, ответ подготовлен
        /// </summary>
        PROCESSED_DOCUMENT,

        /// <summary>
        /// Ошибка обработки
        /// </summary>
        FAILED,

        /// <summary>
        /// Ошибка обработки, ответ
        /// </summary>
        FAILED_RESULT_READY
    }
}
