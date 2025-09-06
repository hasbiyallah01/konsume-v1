using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konsume_v1.Core.Domain.Entities;

namespace konsume_v1.Models
{
    public class MealPlanResponse
    {
        public DateTime Date { get; set; }
        public List<MealPlan> meal { get; set; }
    }
}