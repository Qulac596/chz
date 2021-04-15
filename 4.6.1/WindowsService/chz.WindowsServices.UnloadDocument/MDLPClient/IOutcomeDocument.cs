using chz.WindowsServices.MDLPClient;

namespace chz.WindowsServices.UnloadDocument.MDLPClient
{
    public interface IOutcomeDocument : IDocument
    {
        string DeviceId { get; }

        string SkzkmOriginMsgId { get; }

        string SkzkmReportId { get; }
    }
}