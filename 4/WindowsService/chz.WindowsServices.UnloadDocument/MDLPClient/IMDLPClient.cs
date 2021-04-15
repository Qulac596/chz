using chz.WindowsServices.MDLPClient;
using System.Threading.Tasks;

namespace chz.WindowsServices.UnloadDocument.MDLPClient
{
    public interface IMDLPClient : WindowsServices.MDLPClient.IMDLPClient
    {
        Task CancelSendDocument(string targetId, string document_id, string request_id);

        Task<IOutcomeDocument> GetOutcomeDocumentMetadata(string targetId,string documentId);

        Task<IDocumentId> SendDocument(string targetId,string document, string sign, string requestId);

        Task<ILink> GetDocumentTicketLink(string targetId,string documentId);
    }
}