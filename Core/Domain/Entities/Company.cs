using konsume_v1.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace konsume_v1.Core.Domain.Entities
{
    public class Company : Auditables
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string PhoneNumber {  get; set; }
        public string Wallet {  get; set; }
        public string WebsiteAdress { get; set; }
        public List<CompanyProducts> Products { get; set; }
    }


    public class CompanyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Wallet { get; set; }
        public string WebsiteAdress { get; set; }
    }

    public class CompanyRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Wallet { get; set; }
        public string WebsiteAdress { get; set; }
        public string Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }

    public class CompanyProductRequest 
    {
        public int CompanyId { get; set; }
        public string ProductName { get; set; }
        public string Ingredients { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

}
