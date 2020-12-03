using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.Competitions
{
    public class CategoriesViewModel: IMapFrom<CompetitionCategory>
    {
        public string CategoryName { get; set; }
    }
}
