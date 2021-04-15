namespace WebService.ViewModel
{
    public class Result<TData>
    {
        public Result() { }

        public Result(TData data)
        {
            this.data = data;
        }

        public Result(string errorMessage)
        {
            is_error = true;
            error_message = errorMessage;
        }

        public bool is_error { get; private set; }

        public string error_message { get; private set; }

        public TData data { get; private set; }
    }
}