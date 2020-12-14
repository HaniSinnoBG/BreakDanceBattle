using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.Competitions
{
    public class UsersViewModel : IMapFrom<CompetitionUser>
    {
        public string UserUserName { get; set; }
        public string UserId { get; set; }
    }

}
