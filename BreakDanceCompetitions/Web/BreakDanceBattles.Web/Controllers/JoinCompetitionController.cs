using BreakDanceBattles.Data.Common.Repositories;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Data.Contracts;
using BreakDanceBattles.Web.ViewModels.Competitions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BreakDanceBattles.Web.Controllers
{
    public class JoinCompetitionController : Controller
    {
        private readonly ICompetitionService competitionService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Competition> competitionsRepository;

        public JoinCompetitionController(
            ICompetitionService competitionService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<Competition> competitionsRepository)
        {
            this.competitionService = competitionService;
            this.userManager = userManager;
            this.competitionsRepository = competitionsRepository;
        }
        public IActionResult JoinedCompetitions(int id = 1)
        {
            const int ItemsPerPage = 8;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new CompetitionListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                CompetitionsCount = this.competitionService.GetJoinedCount(userId),
                Competitions = this.competitionService.GetJoinedCompetitions(id, ItemsPerPage, userId),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.competitionService.JoinCompetition(id, userId);
            return this.Redirect($"/Competitions/ById/{id}");

        }
    }
}
