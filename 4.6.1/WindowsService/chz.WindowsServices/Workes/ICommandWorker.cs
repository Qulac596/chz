namespace chz.WindowsServices.Workes
{
    public interface ICommandWorker<TItem> : IWorker
    {
        void Process(TItem item);
    }
}
