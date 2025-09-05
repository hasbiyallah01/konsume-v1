namespace konsume_v1.Core.Domain.Entities
{
    public class MealRecommendation
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string DateSeed { get; set; } 
        public string Meals { get; set; } 
    }
}


