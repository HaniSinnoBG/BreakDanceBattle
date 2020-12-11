using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.Competitions
{
    public class EditCompetitionInputModel: IMapFrom<Competition>
    {
        public int Id { get; set; }
        public string AddedByUserId { get; set; }
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public DateTime DateTime { get; set; }
        public int CountryId { get; set; }
        public IEnumerable<KeyValuePair<string, string>> CountryItems { get; set; }
    }
}
