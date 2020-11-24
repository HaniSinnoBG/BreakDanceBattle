using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Services.Data
{
    public interface ICountriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
