using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konsume_v1.Core.Domain.Enums;

namespace konsume_v1.Models.ProfileModel
{
    public class ProfileResponse
    {
        public int Id { get; set; }
        public Gender Gender { get; set; } = default!;
        public int Height { get; set; } = default!;
        public int Weight { get; set; } = default!;
        public int Age { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? DietType { get; set; } = default!;
        public int UserId { get; set; }
        public string SkinType { get; set; } = default!;
        public ICollection<string> Allergies { get; set; } = new HashSet<string>();
        public ICollection<string> UserGoals { get; set; } = new HashSet<string>();
    }
}