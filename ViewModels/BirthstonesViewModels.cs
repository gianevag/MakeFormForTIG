using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MakeFormForTIG.ViewModels
{
    public class BirthstonesViewModels
    {
        public Boolean isBirthstones { get; set; }

        public List<SelectListItem> Birthstones { get; set;} = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "" },
            new SelectListItem { Value = "necklaces", Text = "Necklaces" },
            new SelectListItem { Value = "earrings", Text = "Earrings" }
        };
    }
}