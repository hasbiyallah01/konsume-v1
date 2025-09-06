using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace konsume_v1.Models.ProfileModel
{
    public class ProfileRequest
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression("Male|Female", ErrorMessage = "Gender must be Male or Female.")]
        public string Gender { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Height must be greater than 0.")]
        public int Height { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Weight must be greater than 0.")]

        public int Weight { get; set; }
        [Required]
        public string SkinType { get; set; } = default!;
        public string Nationality { get; set; } = default!;
        public string? DietType { get; set; } = default!;
        public ICollection<string> Allergies { get; set; } = new HashSet<string>();
        public ICollection<string> UserGoals { get; set; } = new HashSet<string>();  
    }
}