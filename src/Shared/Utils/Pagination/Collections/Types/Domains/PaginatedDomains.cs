using DomainModels.Domains;

namespace Utils.Pagination.Collections.Types.Domains
{
    public class PaginatedDomains : PaginatedCollection<DomainModel>
    {
        public PaginatedDomains(IEnumerable<DomainModel> collection, int totalCount, int page) : base(collection, totalCount, page)
        {
        }
    }
}
