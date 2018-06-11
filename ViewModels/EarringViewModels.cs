using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MakeFormForTIG.ViewModels
{
    public class EarringViewModels
    {
        public Boolean isEarring { get; set; }

        public List<SelectListItem> Earrings { get; set;} = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "" },
            new SelectListItem { Value = "studs", Text = "Studs" },
            new SelectListItem { Value = "threader", Text = "Threader" }
        };
    }
}