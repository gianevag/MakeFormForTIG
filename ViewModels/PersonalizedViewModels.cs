using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MakeFormForTIG.ViewModels
{
    public class PersonalizedViewModels
    {
        public Boolean isPersonalized { get; set; }

        public List<SelectListItem> Personalized { get; set;} = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "" },
            new SelectListItem { Value = "necklaces", Text = "Necklaces" },
            new SelectListItem { Value = "bracelets", Text = "Bracelets" }
        };
    }
}