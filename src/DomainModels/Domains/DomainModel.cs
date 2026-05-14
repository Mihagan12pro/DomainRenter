using System.Text.Json.Serialization;

namespace DomainModels.Domains
{
    public class DomainModel
    {
        public Guid Id { get; }

        public required string Name { get; set; }

        [JsonIgnore]
        public RentedDomainModel RentedDomain { get; set; }
    }
}
