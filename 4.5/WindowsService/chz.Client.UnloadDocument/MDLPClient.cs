using chz.Client.WebApiClient;
using chz.WindowsServices.MDLPClient;
using chz.WindowsServices.UnloadDocument.MDLPClient;
using System.Threading.Tasks;

namespace chz.Client.UnloadDocument
{
    public class MDLPClient : Client.MDLPClient, WindowsServices.UnloadDocument.MDLPClient.IMDLPClient
    {
        public MDLPClient(IClient client, string url) : base(client, url) { }

        /// <summary>
        /// Отправляет документ
        /// Описание api в пункте 5.1 документации
        /// </summary>
        /// <param name="document">
        /// Документ
        /// </param>
        /// <param name="sign">
        /// Подпись документа
        /// </param>
        /// <param name="requestId">
        /// Id запроа
        /// </param>
        /// <returns>
        /// Id документа
        /// </returns>
        public async Task<IDocumentId> SendDocument(string targetId, string document, string sign, string requestId)
        {
            const string url = "documents/send";

            var requestObject = new { document, sign, request_id = requestId };

            return await PostResult<DocumentId>(url, requestObject,targetId);
        }


        /// <summary>
        /// Отмена отправки документа
        /// Описание api в пункте 5.6 документации
        /// </summary>
        /// <param name="document_id">Id документа</param>
        /// <param name="request_id">Id запроса</param>
        public async Task CancelSendDocument(string targetId, string document_id, string request_id)
        {
            const string url = "documents/cancel/";

            var requestObject = new { document_id, request_id };

            await Post(url, requestObject, targetId);
        }

        /// <summary>
        /// Получает методанные исходящего документа
        /// Описание api в пункте 5.9 документации
        /// </summary>
        /// <param name="documentId">
        /// Id документа
        /// </param>
        /// <returns></returns>
        public async Task<IOutcomeDocument> GetOutcomeDocumentMetadata(string targetId, string documentId)
        {
            const string url = "documents/";

            return await GetResult<OutcomeDocument>(url + documentId, targetId);
        }

        /// <summary>
        /// Получает ссылку на квитанцию по номеру исходящего документа
        /// Описание api в пункте 5.12 документации
        /// </summary>
        /// <returns>
        /// Ссылка для скачивания квитанции
        /// </returns>
        public async Task<ILink> GetDocumentTicketLink(string targerId, string documentId)
        {
            const string urlFirstPart = "documents/";
            const string urlLastPart = "/ticket";

            return await GetResult<Link>(urlFirstPart + documentId + urlLastPart, targerId);
        }
    }
}
