using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class AssetServiceDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public VendorId Vendor { get; set; }
        public AssetsServiceTypeId AssetsServiceType { get; set; }
        public int BusinessId { get; set; }
        public AssetsServices ConvertToType()
        {
            return new AssetsServices
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Quantity = Quantity,
                Vendor = Vendor,
                AssetsServiceType = AssetsServiceType,
                BusinessId = BusinessId
            };
        }
    }
}
