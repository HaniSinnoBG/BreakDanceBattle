using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.Competitions
{
    public class CompetitionListViewModel
    {
        public IEnumerable<CompetitionInListViewModel> Competitions { get; set; }

        public int PageNumber { get; set; }
        public bool HasPreviousPage => this.PageNumber > 1;
        public bool HasNextPage => this.PageNumber < this.PagesCount;
        public int PreviousPageNumber => this.PageNumber - 1;
        public int NextPageNumber => this.PageNumber + 1;
        public int PagesCount => (int)Math.Ceiling((double)this.CompetitionsCount / this.ItemsPerPage);
        public int CompetitionsCount { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
