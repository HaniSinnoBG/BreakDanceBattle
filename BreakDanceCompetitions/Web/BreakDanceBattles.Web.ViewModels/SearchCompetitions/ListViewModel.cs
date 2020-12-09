using BreakDanceBattles.Web.ViewModels.Competitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.SearchCompetitions
{
    public class ListViewModel
    {
        public IEnumerable<CompetitionInListViewModel> Competitions { get; set; }
    }
}
