using chz.WindowsServices.MDLPClient;
using System.Threading.Tasks;

namespace chz.WindowsServices.LoadDocument.MDLPClient
{
    public interface IMDLPClient: WindowsServices.MDLPClient.IMDLPClient
    {
        Task<IIncomeDocumentList> GetIncomingDocuments(DocFilter documentFilter, int startFrom, int count);

        Task<ILink> GetDocumentLink(string targetId, string documentId);
    }
}
