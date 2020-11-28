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

        public CompetitionService(
            IDeletableEntityRepository<Competition> competitionsRepository,
            IRepository<Image> imagesRepository)
            {
                this.competitionsRepository = competitionsRepository;
                this.imagesRepository = imagesRepository;
        }

        public async Task CreateAsync(CreateCompetitionInputModel input)
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
            };
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
               .Select(x => new CompetitionInListViewModel 
               {
                Id = x.Id,
                Name = x.Name,
                CountryName = x.Country.Name,
                CountryId = x.CountryId,
                DateTime = x.DateTime.Date.ToString(),
                RemoteImageUrl = x.Image.RemoteImageUrl,
               }).ToList();

            return competitions;
        }
    }
}
