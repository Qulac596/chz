using chz.WindowsServices.UnloadDocument.MDLPClient;
using System.Text.Json.Serialization;

namespace chz.Client.UnloadDocument
{
    /// <summary>
    /// Id документа
    /// </summary>
    internal class DocumentId : IDocumentId
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("document_id")]
        public string Id { get; set; }
    }
}
