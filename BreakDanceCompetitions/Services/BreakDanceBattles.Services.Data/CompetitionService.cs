using BreakDanceBattles.Data.Common.Repositories;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Data.Contracts;
using BreakDanceBattles.Web.ViewModels.Competitions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BreakDanceBattles.Services.Data
{
    public class CompetitionService : ICompetitionService
    {
        private readonly IDeletableEntityRepository<Competition> competitionsRepository;
        public CompetitionService(IDeletableEntityRepository<Competition> competitionsRepository)
            {
                this.competitionsRepository = competitionsRepository;
            }

        public async Task CreateAsync(CreateCompetitionInputModel input)
        {
            
            var competition = new Competition
            {
                Name = input.Name,
                Description = input.Description,
                DateTime = input.DateTime,
                CountryId = input.CountryId,
               
            };
            
            await this.competitionsRepository.AddAsync(competition);
            await this.competitionsRepository.SaveChangesAsync();
        }
    }
}
