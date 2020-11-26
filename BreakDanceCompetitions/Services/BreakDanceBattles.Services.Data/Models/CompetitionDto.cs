using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Services.Data.Models
{
    public class CompetitionDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public string DateTime { get; set; }

        public int CountryId { get; set; }
    }
}
 