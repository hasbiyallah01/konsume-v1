using System.ComponentModel.DataAnnotations.Schema;

namespace konsume_v1.Core.Domain.Entities
{
    public class UserInteraction :Auditables
    {
        public int UserId { get; set; }
        public virtual User User { get; set; } = default!;
        public string Question { get; set; }
        public string Response { get; set; }
    }

}
