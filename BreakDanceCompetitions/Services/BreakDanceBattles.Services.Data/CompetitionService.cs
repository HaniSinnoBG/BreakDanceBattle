namespace BreakDanceBattles.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using BreakDanceBattles.Data.Common.Models;
    using BreakDanceBattles.Data.Common.Repositories;
    using BreakDanceBattles.Data.Models;
    using BreakDanceBattles.Services.Data.Contracts;
    using BreakDanceBattles.Services.Mapping;
    using BreakDanceBattles.Web.ViewModels.Competitions;
    using Microsoft.AspNetCore.Identity;

    public class CompetitionService : ICompetitionService
    {

        private readonly string[] AllowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Competition> competitionsRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRespository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<CompetitionUser> joinedRepository;

        public CompetitionService(
            IDeletableEntityRepository<Competition> competitionsRepository,
            IRepository<Image> imagesRepository,
            IDeletableEntityRepository<Category> categoriesRespository,
            UserManager<ApplicationUser> userManager,
            IRepository<CompetitionUser> joinedRepository)
            {
                this.competitionsRepository = competitionsRepository;
                this.imagesRepository = imagesRepository;
            this.categoriesRespository = categoriesRespository;
            this.userManager = userManager;
            this.joinedRepository = joinedRepository;
        }

        public async Task CreateAsync(CreateCompetitionInputModel input, string userId, string imagePath)
        {

            var competition = new Competition
            {
                Name = input.Name,
                Description = input.Description,
                DateTime = input.DateTime,
                CountryId = input.CountryId,
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

            Directory.CreateDirectory($"{imagePath}/competitions");
            var dbImage = new Image();
            
            if (input.ImageUrl == null)
            {
                var extension = Path.GetExtension(input.Image.FileName);
                extension = extension.TrimStart('.');

                if (!this.AllowedExtensions.Any(x=> extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                dbImage.AddedByUserId = userId;
                dbImage.Extension = extension;
                competition.Image = dbImage;
           
                var physicalPath = $"{imagePath}/competitions/{dbImage.Id}.{extension}";
                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await input.Image.CopyToAsync(fileStream);
                }
            }
            else
            {
                dbImage.AddedByUserId = userId;
                dbImage.RemoteImageUrl = input.ImageUrl;
                competition.Image = dbImage;
            }

            dbImage.CompetitionId = competition.Id;
            await this.imagesRepository.AddAsync(dbImage);
            await this.competitionsRepository.AddAsync(competition);
            await this.competitionsRepository.SaveChangesAsync();
        }

        public IEnumerable<CompetitionInListViewModel> GetAll(int page, int itemsPerPage)
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

        public IEnumerable<CompetitionInListViewModel> GetMyCompetitions(int page, int itemsPerPage, string userId) 
        {

            var myCompetitions = this.competitionsRepository.AllAsNoTracking()
                .Where(x => x.AddedByUserId == userId)
                .OrderByDescending(x => x.Id)
                .Skip((page-1) * itemsPerPage).Take(itemsPerPage)
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

        public IEnumerable<T> GetByCategories<T>(IEnumerable<int> categoryIds)
        {
            var selected = new List<T>();

            foreach (var categoryId in categoryIds)
            {
                var query = this.competitionsRepository.All().AsQueryable();
                query = query.Where(x => x.Categories.Any(i => i.CategoryId == categoryId));
                var queried = query.To<T>();

                foreach (var item in queried)
                {
                    if (!selected.Contains(item))
                    {
                        selected.Add(item);
                    }

                }
            }

            return selected;

        }
        public async Task JoinCompetition(int id, string userId )
        {
            var competition = this.competitionsRepository.All().FirstOrDefault(x => x.Id == id);
            var user = await this.userManager.FindByIdAsync(userId);
            var CompetitionUser = new CompetitionUser();
            CompetitionUser.Competition = competition;
            CompetitionUser.User = user;

            if (!joinedRepository.All().Any(x => x.UserId == userId && x.CompetitionId == id))
            {
            competition.JoinedUsers.Add(CompetitionUser);
            await this.competitionsRepository.SaveChangesAsync();
            }

        }
        public IEnumerable<CompetitionInListViewModel> GetJoinedCompetitions(int page, int itemsPerPage, string userId)
        {

            var myCompetitions = this.competitionsRepository.AllAsNoTracking()
                .Where(x => x.JoinedUsers.Any(i => i.UserId == userId))
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<CompetitionInListViewModel>()
                .ToList();
            return myCompetitions;

        }

        public int GetCount(string userId)
        {
            return this.competitionsRepository.AllAsNoTracking()
                .Where(x => x.AddedByUserId == userId)
                .Count();
        }

        public int GetJoinedCount(string userId)
        {
            return this.competitionsRepository.AllAsNoTracking()
                .Where(x => x.JoinedUsers.Any(i => i.UserId == userId))
                .Count();
        }

        public int CountAll()
        {
            return this.competitionsRepository.AllAsNoTracking().Count();
        }

        public int CountAllUpcoming()
        {
            return this.competitionsRepository.AllAsNoTracking()
                .Where(x => x.DateTime >= DateTime.Today)
                .Count();
        }

        public int BattlesToday()
        {
            return this.competitionsRepository.AllAsNoTracking()
                .Where(x => x.DateTime.Date == DateTime.Today.Date)
                .Count();
        }
    }
}
