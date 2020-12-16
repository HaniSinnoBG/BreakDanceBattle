namespace BreakDanceBattles.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using BreakDanceBattles.Services.Data.Contracts;
    using BreakDanceBattles.Web.ViewModels;
    using BreakDanceBattles.Web.ViewModels.Competitions;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICompetitionService competitionService;

        public HomeController(ICompetitionService competitionService)
        {
            this.competitionService = competitionService;
        }
        public IActionResult Index()
        {

            var viewModel = new HomeCompetitionsViewModel
            {
                Competitions = this.competitionService.GetAll(1, 100),
                UpcomingCompetitions = this.competitionService.CountAllUpcoming(),
                CompetitionsToday = this.competitionService.BattlesToday(),
                CompetitionsCount = this.competitionService.CountAll(),
            };

            return this.View(viewModel);
        }
        
    }
   
}
