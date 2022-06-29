namespace TVShowTraker.Models.Filters
{
    public class TVShowFilter: BaseFilter
    {
        public string TVShowName { get; set; } = string.Empty;
        public int GenreId { get; set; } = 0;
    }
}
