using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BreakDanceBattles.Data.Common.Repositories;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Data;
using BreakDanceBattles.Services.Data.Contracts;
using BreakDanceBattles.Web.ViewModels.Competitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BreakDanceBattles.Web.Controllers
{
    public class CompetitionsController : Controller
    {
        private readonly ICompetitionService competitionService;
        private readonly ICountriesService countriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<Competition> competitionsRepository;

        public CompetitionsController(
            ICompetitionService competitionService, 
            ICountriesService countriesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IDeletableEntityRepository<Competition> competitionsRepository)
        {
            this.competitionService = competitionService;
            this.countriesService = countriesService;
            this.userManager = userManager;
            this.environment = environment;
            this.competitionsRepository = competitionsRepository;
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
            try
            {
                await this.competitionService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception)
            {
                var viewModel = new CreateCompetitionInputModel();
                viewModel.CountryItems = this.countriesService.GetAllAsKeyValuePairs();
                return this.View(viewModel);
            }
                

      
            return this.Redirect("/");
        }   
        [Authorize]
        public IActionResult Edit(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var inputModel = this.competitionService.GetById<EditCompetitionInputModel>(id);
            inputModel.CountryItems = this.countriesService.GetAllAsKeyValuePairs();
            if (inputModel.AddedByUserId == userId)
            {
                return this.View(inputModel);
            }
            else
            {
                return this.Redirect("/Competitions/All");
            }


        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditCompetitionInputModel input)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (input.AddedByUserId == userId )
            {
                if (!this.ModelState.IsValid)
                {
                    input.CountryItems = this.countriesService.GetAllAsKeyValuePairs();
                    return this.View(input);
                }
                await this.competitionService.UpdateAsync(id, input);
                return this.RedirectToAction(nameof(this.ById), new { id });
            }
            else
            {
                return this.Redirect("/Competitions/All");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var competition = this.competitionsRepository.All().FirstOrDefault(x => x.Id == id);
            if (competition.AddedByUserId == userId)
            {
                await this.competitionService.DeleteAsync(id);
                return this.Redirect("/Competitions/MyCompetitions");
            }
            else
            {
                return this.Redirect("/Competitions/MyCompetitions");
            }

        }
        public IActionResult All()
        {
            var viewModel = new CompetitionListViewModel 
            {
                PageNumber = 1,
                Competitions = this.competitionService.GetAll(1,int.MaxValue),
                
            };
            return this.View(viewModel);
        }
        public IActionResult ById(int id)
        {
            var competition = this.competitionService.GetById<SingleCompetitionViewModel>(id);
            return this.View(competition);
        }
        public IActionResult MyCompetitions(int id = 1) 
        {
            const int ItemsPerPage = 12;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new CompetitionListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                CompetitionsCount = this.competitionService.GetCount(userId),
                Competitions = this.competitionService.GetMyCompetitions(id, ItemsPerPage, userId),
            };
 
            return this.View(viewModel);
        }
        public IActionResult JoinedCompetitions(int id = 1)
        {
            const int ItemsPerPage = 12;
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
