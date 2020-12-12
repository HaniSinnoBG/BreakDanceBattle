namespace BreakDanceBattles.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BreakDanceBattles.Web.ViewModels.Competitions;
    public interface ICompetitionService
    {
        Task CreateAsync(CreateCompetitionInputModel input, string userId, string imagePath);
        IEnumerable<CompetitionInListViewModel> GetAll(int page, int itemsPerPage = 12);
        T GetById<T>(int id);
        IEnumerable<CompetitionInListViewModel> GetMyCompetitions(string id);
        Task UpdateAsync(int id, EditCompetitionInputModel input);
        Task DeleteAsync(int id);
        IEnumerable<T> GetByCategories<T>(IEnumerable<int> categoryIds);
        Task JoinCompetition(int id, string userId);
    }
}
