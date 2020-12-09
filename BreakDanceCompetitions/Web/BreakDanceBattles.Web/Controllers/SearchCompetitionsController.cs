using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakDanceBattles.Services.Data;
using BreakDanceBattles.Services.Data.Contracts;
using BreakDanceBattles.Web.ViewModels.Competitions;
using BreakDanceBattles.Web.ViewModels.SearchCompetitions;
using Microsoft.AspNetCore.Mvc;

namespace BreakDanceBattles.Web.Controllers
{
    public class SearchCompetitionsController : BaseController
    {
        private readonly ICompetitionService competitionService;

        public SearchCompetitionsController(
            ICompetitionService competitionService,
            ICategoriesService categoriesService)
        {
            this.competitionService = competitionService;
            CategoriesService = categoriesService;
        }

        public ICategoriesService CategoriesService { get; }

        public IActionResult Index()
        {
            //list of all categories
            var viewModel = new SearchIndexViewModel
            {
                Categories = this.CategoriesService.GetAll<CategoryNameIdViewModel>(),
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult List(SearchListInputModel input)
        {
            var viewModel = new ListViewModel
            {
                Competitions = this.competitionService.GetByCategories<CompetitionInListViewModel>(input.Categories),
            };
            return this.View(viewModel);
        }
    }
}
