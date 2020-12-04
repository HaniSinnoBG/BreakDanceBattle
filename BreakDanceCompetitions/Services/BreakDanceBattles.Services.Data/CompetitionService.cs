using BreakDanceBattles.Data.Common.Repositories;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Data.Contracts;
using BreakDanceBattles.Services.Mapping;
using BreakDanceBattles.Web.ViewModels.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakDanceBattles.Services.Data
{
    public class CompetitionService : ICompetitionService
    {
        private readonly IDeletableEntityRepository<Competition> competitionsRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRespository;

        public CompetitionService(
            IDeletableEntityRepository<Competition> competitionsRepository,
            IRepository<Image> imagesRepository,
            IDeletableEntityRepository<Category> categoriesRespository)
            {
                this.competitionsRepository = competitionsRepository;
                this.imagesRepository = imagesRepository;
            this.categoriesRespository = categoriesRespository;
        }

        public async Task CreateAsync(CreateCompetitionInputModel input, string userId)
        {
            var image = new Image
            {
                RemoteImageUrl = input.ImageUrl,
                
            };
            
            var competition = new Competition
            {
                Name = input.Name,
                Description = input.Description,
                DateTime = input.DateTime,
                CountryId = input.CountryId,
                Image = image,
                AddedByUserId = userId,
            };
            foreach (var inputCategory in input.Categories)
            {
                var category = this.categoriesRespository.All()
                    .FirstOrDefault(x => x.Name == inputCategory.CategoryName);
                if (category==null)
                {
                    category = new Category { Name = inputCategory.CategoryName };
                }
                competition.Categories.Add(new CompetitionCategory
                {
                    Category = category,
                    Competition = competition,
                });
            }
            image.CompetitionId = competition.Id;
            await this.imagesRepository.AddAsync(image);
            await this.competitionsRepository.AddAsync(competition);
            await this.competitionsRepository.SaveChangesAsync();
        }

        public IEnumerable<CompetitionInListViewModel> GetAll(int page, int itemsPerPage = 12)
        {
            var competitions = this.competitionsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip(0).Take(itemsPerPage)
               .To<CompetitionInListViewModel>()
               .ToList();

            return competitions;
        }

        public T GetById<T>(int id)
        {
            var competition = this.competitionsRepository.AllAsNoTracking()
                 .Where(x => x.Id == id)
                 .To<T>().FirstOrDefault();
            return competition;
        }
    }
}
