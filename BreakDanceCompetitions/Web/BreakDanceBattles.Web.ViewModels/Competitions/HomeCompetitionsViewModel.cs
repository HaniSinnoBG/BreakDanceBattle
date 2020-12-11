using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.Competitions
{
    public class HomeCompetitionsViewModel
    {
        public IEnumerable<CompetitionInListViewModel> Competitions { get; set; }
    }
}
