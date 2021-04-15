using chz.WindowsServices.MDLPClient;

namespace chz.WindowsServices.LoadDocument.MDLPClient
{
    public interface IIncomeDocument : IDocument
    {
        string SenderSysId { get; }
    }
}