using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MakeFormForTIG.ViewModels
{
    public class OrderInFirstPageViewModel
    {
        public Boolean isFirstPage { get; set; }
        public List<SelectListItem> OrderInFirstPage { get; set;} = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "" },
            new SelectListItem { Value = "1", Text = "1" },
            new SelectListItem { Value = "2", Text = "2" },
            new SelectListItem { Value = "3", Text = "3" },
            new SelectListItem { Value = "4", Text = "4" }
        };
    }
}