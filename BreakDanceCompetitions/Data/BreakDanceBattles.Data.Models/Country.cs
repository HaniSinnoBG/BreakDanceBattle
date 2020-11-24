using BreakDanceBattles.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Data.Models
{
    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Competitions = new HashSet<Competition>();
        }
        public string Name { get; set; }
        public virtual ICollection<Competition> Competitions { get; set; }
    }
}
