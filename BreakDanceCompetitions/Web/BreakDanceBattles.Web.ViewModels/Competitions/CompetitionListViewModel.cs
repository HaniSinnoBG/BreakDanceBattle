using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.Competitions
{
    public class CompetitionListViewModel
    {
        public IEnumerable<CompetitionInListViewModel> Competitions{ get; set; }

        public int PageNumber { get; set; }
    }
}
