namespace chz.WindowsServices.ThisLoadDocument.MDLPClient
{
    public interface IIncomeDocumentList
    {
        int Count { get;}

        IncomeDocument[] Documents { get;}
    }
}