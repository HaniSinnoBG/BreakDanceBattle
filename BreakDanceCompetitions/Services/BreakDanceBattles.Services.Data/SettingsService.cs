namespace BreakDanceBattles.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using BreakDanceBattles.Data.Common.Repositories;
    using BreakDanceBattles.Data.Models;
    using BreakDanceBattles.Services.Mapping;

    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount()
        {
            return this.settingsRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.settingsRepository.All().To<T>().ToList();
        }
    }
}
