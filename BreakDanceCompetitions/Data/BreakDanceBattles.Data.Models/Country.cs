using BreakDanceBattles.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Data.Models
{
    public class Country : BaseDeletableModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
