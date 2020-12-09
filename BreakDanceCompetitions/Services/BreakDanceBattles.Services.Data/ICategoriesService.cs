using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Services.Data
{
    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>();
    }
}
