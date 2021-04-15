using chz.Client.WebApiClient;
using chz.WindowsServices.MDLPClient;
using System.Threading.Tasks;

namespace chz.Client.LoadDocument
{
    public class MDLPClient : Client.MDLPClient, WindowsServices.LoadDocument.MDLPClient.IMDLPClient
    {

        public MDLPClient(IClient client, string url) : base(client, url) { }

        /// <summary>
        /// Возвращает список входящих документов
        /// Описание api в пункте 5.8 документации
        /// </summary>
        /// <param name="documentFilter">Фильтр списка документов</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых документов</param>
        /// <param name="count">Количество записей в списке возвращаемых документов</param>
        /// <returns>Список входящих документов</returns>
        public async Task<WindowsServices.LoadDocument.MDLPClient.IIncomeDocumentList> GetIncomingDocuments(WindowsServices.LoadDocument.MDLPClient.DocFilter documentFilter, int startFrom, int count)
        {
            const string url = "documents/income";

            var requestObject = new { filter = documentFilter, start_from = startFrom, count };

            return await PostResult<WindowsServices.LoadDocument.MDLPClient.IncomeDocumentList>(url, requestObject);
        }

        /// <summary>
        /// Получает документ по индификатору
        /// Описание api пункте 5.10 документации
        /// </summary>
        /// <param name="documentId">Id документа</param>
        /// <returns>Ссылка для загрузки</returns>
        public async Task<ILink> GetDocumentLink(string targerId, string documentId)
        {
            const string url = "documents/download/";

            return await GetResult<Link>(url + documentId, targerId);
        }
    }
}
