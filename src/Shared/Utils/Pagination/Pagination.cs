namespace Utils.Pagination
{
    public class Pagination<T>
    {
        public int Page { get; set; }

        public int Size { get; set; }

        public IEnumerable<T> Apply(IEnumerable<T> data)
        {
            var paginatedData = data.Skip((Page - 1) * Size)
                .Take(Size);

            return paginatedData;
        }

        public Pagination(int page = 1, int size = 5)
        {
            Page = page;

            Size = size;
        }
    }
}
