﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BreakDanceBattles.Web.ViewModels.Competitions
{
    public class CreateCompetitionInputModel
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        [MinLength(20)]
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public int CountryId { get; set; }



        public IEnumerable<CompetitionCategoryInputModel> Categories { get; set; }
        public IEnumerable<KeyValuePair<string, string>> CountryItems { get; set; }
    }
}