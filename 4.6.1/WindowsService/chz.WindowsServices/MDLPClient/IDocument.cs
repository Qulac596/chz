using chz.Common;
using System;

namespace chz.WindowsServices.MDLPClient
{
    public interface IDocument
    {
        string RequestId { get; }

        string DocumentId { get; }

        DateTime Date { get; }

        DateTime ProcessedDate { get; }

        string Sender { get; }

        string Receiver { get; }

        string SysId { get; }

        int? DocType { get; }

        MDLPDocumentStatus Status { get; }

        int? FileUploadtype { get; }

        string Version { get; set; }
    }
}