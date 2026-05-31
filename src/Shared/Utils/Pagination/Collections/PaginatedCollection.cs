namespace Utils.Pagination.Collections
{
    public class PaginatedCollection<T>
    {
        private IEnumerable<T> _collection;

        public IEnumerable<T> Collection
            => _collection; 

        public int TotalCount { get; }

        public int Page { get; }

        public int CountOnPage { get; }

        public PaginatedCollection(
            IEnumerable<T> collection, 
            int totalCount,
            int page)
        {
            TotalCount = totalCount;

            Page = page;

            _collection = collection;

            CountOnPage = collection.Count();
        }
    }
}
