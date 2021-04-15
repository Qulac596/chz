using chz.WindowsServices.MDLPClient;

namespace chz.WindowsServices.ThisLoadDocument.MDLPClient
{
    public interface IIncomeDocument : IDocument
    {
        string SenderSysId { get; }
    }
}