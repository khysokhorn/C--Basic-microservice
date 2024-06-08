namespace Model
{
    public class DataPagination<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public T Data { get; set; }
    }
}