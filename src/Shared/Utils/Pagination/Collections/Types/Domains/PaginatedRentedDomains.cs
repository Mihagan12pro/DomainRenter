using DomainModels.Domains;

namespace Utils.Pagination.Collections.Types.Domains
{
    public class PaginatedRentedDomains : PaginatedCollection<RentedDomainModel>
    {
        public PaginatedRentedDomains(IEnumerable<RentedDomainModel> collection, int totalCount, int page) : base(collection, totalCount, page)
        {
        }
    }
}
