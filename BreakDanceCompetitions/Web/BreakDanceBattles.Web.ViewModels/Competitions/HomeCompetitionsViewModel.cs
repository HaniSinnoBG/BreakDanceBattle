using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.Competitions
{
    public class HomeCompetitionsViewModel
    {
        public IEnumerable<CompetitionInListViewModel> Competitions { get; set; }
        public int CompetitionsCount { get; set; }
        public int UpcomingCompetitions { get; set; }
        public int CompetitionsToday { get; set; }
    }
}
