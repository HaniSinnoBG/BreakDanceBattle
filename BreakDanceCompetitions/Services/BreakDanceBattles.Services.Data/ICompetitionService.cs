namespace BreakDanceBattles.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BreakDanceBattles.Web.ViewModels.Competitions;
    public interface ICompetitionService
    {
        Task CreateAsync(CreateCompetitionInputModel input);
        IEnumerable<CompetitionInListViewModel> GetAll(int page, int itemsPerPage = 12);
    }
}
