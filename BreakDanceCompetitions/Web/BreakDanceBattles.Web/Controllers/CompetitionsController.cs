using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakDanceBattles.Services.Data.Contracts;
using BreakDanceBattles.Web.ViewModels.Competitions;
using Microsoft.AspNetCore.Mvc;

namespace BreakDanceBattles.Web.Controllers
{
    public class CompetitionsController : Controller
    {
        private readonly ICompetitionService competitionService;

        public CompetitionsController(ICompetitionService competitionService)
        {
            this.competitionService = competitionService;
        }
        public IActionResult Create()
        {
            var viewModel = new CreateCompetitionInputModel();
            return this.View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCompetitionInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                return this.View();
            }
            return this.Redirect("/");
        }
    }
}
