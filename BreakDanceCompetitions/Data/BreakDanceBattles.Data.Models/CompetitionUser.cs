using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Data.Models
{
    public class CompetitionUser
    {
        public int Id { get; set; }

        public int CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
