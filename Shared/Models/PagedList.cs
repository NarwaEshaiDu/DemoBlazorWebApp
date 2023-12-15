namespace BlazorApp2.Appllication.Models
{
    public class PagedList<T>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int Count { get; set; }
        public List<T> Data { get; set; } = new();
    }
}
