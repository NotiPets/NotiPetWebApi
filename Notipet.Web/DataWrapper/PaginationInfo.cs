namespace Notipet.Web.DataWrapper
{
    public class PaginationInfo
    {
        const int MaxPageSize = 500;
        private int _itemCount;
        public int ItemCount
        {
            get => _itemCount;
            set => _itemCount = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int StartAt { get; set; } 
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public PaginationInfo(int itemCount, int page, int totalItems)
        {
            ItemCount = itemCount;
            TotalItems = totalItems;
            CurrentPage = (page < 0) ? 1 : page;
            TotalPages = (int)Math.Ceiling(totalItems / (double)itemCount);
            StartAt = (page - 1) * itemCount;
        }
    }
}
