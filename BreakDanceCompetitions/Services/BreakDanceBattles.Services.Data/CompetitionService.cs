﻿using BreakDanceBattles.Data.Common.Repositories;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Data.Common.Models;
using BreakDanceBattles.Services.Data.Contracts;
using BreakDanceBattles.Services.Mapping;
using BreakDanceBattles.Web.ViewModels.Competitions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BreakDanceBattles.Services.Data
{
    public class CompetitionService : ICompetitionService
    {
        private readonly IDeletableEntityRepository<Competition> competitionsRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRespository;
        private readonly UserManager<ApplicationUser> userManager;

        public CompetitionService(
            IDeletableEntityRepository<Competition> competitionsRepository,
            IRepository<Image> imagesRepository,
            IDeletableEntityRepository<Category> categoriesRespository,
            UserManager<ApplicationUser> userManager
            )
            {
                this.competitionsRepository = competitionsRepository;
                this.imagesRepository = imagesRepository;
            this.categoriesRespository = categoriesRespository;
            this.userManager = userManager;
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

        public IEnumerable<CompetitionInListViewModel> GetMyCompetitions(string userId) 
        {

            var myCompetitions = this.competitionsRepository.AllAsNoTracking()
                .Where(x => x.AddedByUserId == userId)
                .OrderByDescending(x => x.Id)
               .To<CompetitionInListViewModel>()
               .ToList();
            return myCompetitions;
        }

        public async Task UpdateAsync(int id, EditCompetitionInputModel input)
        {
            var competition = this.competitionsRepository.All().FirstOrDefault(x => x.Id == id);
            competition.Name = input.Name;
            competition.Description = input.Description;
            competition.CountryId = input.CountryId;
            competition.DateTime = input.DateTime;
            await this.competitionsRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var competition = this.competitionsRepository.All().FirstOrDefault(x => x.Id == id);
            this.competitionsRepository.Delete(competition);
            await this.competitionsRepository.SaveChangesAsync();
        }

    }
}
