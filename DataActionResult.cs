namespace MauiAppEmbedIO
{
    public class DataActionResult<T>
    {
        public bool Successful { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
    }

}
