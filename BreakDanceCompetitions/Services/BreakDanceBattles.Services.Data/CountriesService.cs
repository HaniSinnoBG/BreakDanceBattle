using BreakDanceBattles.Data.Common.Repositories;
using BreakDanceBattles.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakDanceBattles.Services.Data
{
    public class CountriesService : ICountriesService
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public CountriesService(IDeletableEntityRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }
        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.countriesRepository.All().Select(x => new
            {
                x.Id,
                x.Name
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
