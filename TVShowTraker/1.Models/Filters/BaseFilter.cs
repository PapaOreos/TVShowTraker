namespace TVShowTraker.Models.Filters
{
    public class BaseFilter
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public bool IsValid()
        {
            if (CurrentPage <= 0) return false;
            if(PageSize <= 0) return false;

            return true;
        }
    }
}
