using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace konsume_v1GenAi.Core.Domain.Entities
{

    public class ScrambledMessageMapping
    {
        [Key]
        public int Id { get; set; }
        public string ScrambledGuid { get; set; } = default!;
        [ForeignKey(nameof(Product))]
        public int ProductEntityId { get; set; }
        public Product Product { get; set; }
    }

    public class Product
    {
        [Key]
        public int Id { get; set; }

        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Usage { get; set; }
        
        public string HealthCompatibility { get; set; }
        public string HealthImpactAndRisk { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public ICollection<ProductAlternative> Alternatives { get; set; } = new List<ProductAlternative>();
    }

    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }
        public string RiskLevel { get; set; }
        public string Usage { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public List<string> Concerns { get; set; } = new List<string>();
    }

    public class ProductAlternative
    {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; }
        public int Rating { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }


    public class ProductHealthAnalysis
    {
        public string HealthCompatibility { get; set; }
        public string HealthImpactAndRisk { get; set; }
    }


    public class ProductRecommendationResponse
    {
        public List<ProductAlternative> Alternatives { get; set; } 
        public ProductHealthAnalysis HealthAnalysis { get; set; }
    }

    public class ProductData
    {
        public string Code { get; set; }
        public string CodeType { get; set; }
        public Product Product { get; set; }
        public bool Inferred { get; set; }
    }
}
