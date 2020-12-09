using BreakDanceBattles.Data.Common.Repositories;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakDanceBattles.Services.Data
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }
        public IEnumerable<T> GetAll<T>()
        {
            return this.categoriesRepository.All().To<T>().ToList();
        }
    }
}
