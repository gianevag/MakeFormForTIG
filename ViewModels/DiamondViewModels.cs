using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MakeFormForTIG.ViewModels
{
    public class DiamondViewModels
    {
        public Boolean isDiamond { get; set; }

        public List<SelectListItem> Diamond { get; set;} = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "" },
            new SelectListItem { Value = "necklaces", Text = "Necklaces" },
            new SelectListItem { Value = "earrings", Text = "Earrings" },
            new SelectListItem { Value = "bracelets", Text = "Bracelets" },
            new SelectListItem { Value = "rings", Text = "Rings" }
        };
    }
}