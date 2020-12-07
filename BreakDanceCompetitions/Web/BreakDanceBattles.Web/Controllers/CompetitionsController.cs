using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Data;
using BreakDanceBattles.Services.Data.Contracts;
using BreakDanceBattles.Web.ViewModels.Competitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BreakDanceBattles.Web.Controllers
{
    public class CompetitionsController : Controller
    {
        private readonly ICompetitionService competitionService;
        private readonly ICountriesService countriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CompetitionsController(
            ICompetitionService competitionService, 
            ICountriesService countriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.competitionService = competitionService;
            this.countriesService = countriesService;
            this.userManager = userManager;
        }
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateCompetitionInputModel();
            viewModel.CountryItems = this.countriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCompetitionInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountryItems = this.countriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }
            var user = await this.userManager.GetUserAsync(this.User);
            await this.competitionService.CreateAsync(input, user.Id);
            return this.Redirect("/");
        }   
        [Authorize]
        public IActionResult Edit(int id)
        {
            var inputModel = this.competitionService.GetById<EditCompetitionInputModel>(id);
            inputModel.CountryItems = this.countriesService.GetAllAsKeyValuePairs();
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditCompetitionInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountryItems = this.countriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }
            await this.competitionService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }
        public IActionResult All(int id)
        {
            var viewModel = new CompetitionListViewModel 
            {
                PageNumber = id,
                Competitions = this.competitionService.GetAll(id,12),
                
            };
            return this.View(viewModel);
        }
        public IActionResult ById(int id)
        {
            var competition = this.competitionService.GetById<SingleCompetitionViewModel>(id);
            return this.View(competition);
        }
        public IActionResult MyCompetitions(int id) 
        {        
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new CompetitionListViewModel
            {
                PageNumber = 1,
                Competitions = this.competitionService.GetMyCompetitions(userId),
            };
 
            return this.View(viewModel);
        }
    }
}
