namespace konsume_v1.Core.Domain.Entities
{
    public class Streak : Auditables
    {
        public int ProfileId { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public int StreakCount { get; set; }

    }
}
