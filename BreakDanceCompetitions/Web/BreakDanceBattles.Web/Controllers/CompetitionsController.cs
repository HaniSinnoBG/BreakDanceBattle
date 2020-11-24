using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakDanceBattles.Services.Data;
using BreakDanceBattles.Services.Data.Contracts;
using BreakDanceBattles.Web.ViewModels.Competitions;
using Microsoft.AspNetCore.Mvc;

namespace BreakDanceBattles.Web.Controllers
{
    public class CompetitionsController : Controller
    {
        private readonly ICompetitionService competitionService;
        private readonly ICountriesService countriesService;

        public CompetitionsController(ICompetitionService competitionService, ICountriesService countriesService)
        {
            this.competitionService = competitionService;
            this.countriesService = countriesService;
        }
        public IActionResult Create()
        {
            var viewModel = new CreateCompetitionInputModel();
            viewModel.CountryItems = this.countriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCompetitionInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountryItems = this.countriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }
            await this.competitionService.CreateAsync(input);
            return this.Redirect("/");
        }
    }
}
