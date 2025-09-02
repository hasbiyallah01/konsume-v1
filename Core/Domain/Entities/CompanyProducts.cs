using konsume_v1.Core.Domain.Entities;

namespace konsume_v1.Core.Domain.Entities
{
    public class CompanyProducts : Auditables
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string ProductName { get; set; }
        public bool IsOriginal { get; set; }
        public string Hash { get; set; }
        public string BatchId { get; set; }
        public string Ingredients { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class CompanyProductsResponse
    {

        public string BatchId { get; set; }
        public CompanyProdRes ProdRes { get; set; }
    }

    public class CompanyProdRes
    {
        public int CompanyId { get; set; }
        public string ProductName { get; set; }
        public bool IsOriginal { get; set; }
        public string Hash { get; set; }
        public string Ingredients { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
