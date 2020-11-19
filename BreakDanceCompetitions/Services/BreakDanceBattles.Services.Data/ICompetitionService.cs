namespace BreakDanceBattles.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using BreakDanceBattles.Web.ViewModels.Competitions;
    public interface ICompetitionService
    {
        Task CreateAsync(CreateCompetitionInputModel input);
    }
}
