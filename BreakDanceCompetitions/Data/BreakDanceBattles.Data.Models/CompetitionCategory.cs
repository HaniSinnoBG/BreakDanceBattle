using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Data.Models
{
    public class CompetitionCategory
    {
        public int Id { get; set; }

        public int CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
