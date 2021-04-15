namespace chz.WindowsServices.LoadDocument.MDLPClient
{
    public interface IIncomeDocumentList
    {
        int Count { get;}

        IncomeDocument[] Documents { get;}
    }
}