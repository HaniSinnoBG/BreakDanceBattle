using BreakDanceBattles.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Data.Models
{
    public class Competition : BaseDeletableModel<int>
    {
        public Competition()
        {
            this.Categories = new HashSet<CompetitionCategory>();
            this.JoinedUsers = new HashSet<CompetitionUser>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<CompetitionCategory> Categories { get; set; }

        public virtual ICollection<CompetitionUser> JoinedUsers { get; set; }

        public int ImageId { get; set; }

        public virtual Image Image { get; set; }




    }
}
