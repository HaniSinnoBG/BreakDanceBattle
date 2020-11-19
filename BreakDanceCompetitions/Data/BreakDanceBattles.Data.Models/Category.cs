using BreakDanceBattles.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Data.Models
{
    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Competitions = new HashSet<CompetitionCategory>();
        }

        public string Name { get; set; }

        public virtual ICollection<CompetitionCategory> Competitions { get; set; }
    }
}
