using BreakDanceBattles.Data.Common.Repositories;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Data.Contracts;
using BreakDanceBattles.Web.ViewModels.Competitions;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BreakDanceBattles.Services.Data.Tests
{
   public  class ControllerServiceTests
    {
        private readonly ICompetitionService competitionService;
        private IDeletableEntityRepository<Competition> competitionsRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRespository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<CompetitionUser> joinedRepository;

        public ControllerServiceTests()
        {
            this.competitionService = new CompetitionService(this.competitionsRepository
                ,this.imagesRepository
                ,this.categoriesRespository
                ,this.userManager
                ,this.joinedRepository);
        }
        [Fact]
        public async Task TestAddingCompetition() 
        {
            var categories = new List<CompetitionCategoryInputModel>();
            var category = new CompetitionCategoryInputModel { CategoryName ="2vs2" };
            categories.Add(category);
            var model = new CreateCompetitionInputModel
            {
               
                Name = "Name",
                DateTime = DateTime.Today,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                ImageUrl = "url",
                CountryId = 2,
                Categories = categories
            };
            await this.competitionService.CreateAsync(model, "id", "path");
            var count =  this.competitionsRepository.All().Count();

            Assert.Equal(1, count);
        }
        [Fact]
        public void  GetAllShouldReturnCorrectAmountOfCompetitions() 
        {
            var list = new List<Competition>();
            var MockRepo = new Mock<IDeletableEntityRepository<Competition>>();
            MockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());

            //var service = new CompetitionService(MockRepo.Object, this.competitionsRepository);
        }
    }
}
