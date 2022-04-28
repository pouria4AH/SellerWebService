namespace SellerWebService.DataLayer.DTOs.Identity
{
    public class UserClaimsStore
    {
        public string Mobile { get; set; }
        public string Role { get; set; }
        public Guid UniqueCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
