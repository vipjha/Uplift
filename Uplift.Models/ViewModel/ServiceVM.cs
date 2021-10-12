using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uplift.Models.ViewModel
{
    public class ServiceVM
    {
        public Service Service { get; set; }
        public IEnumerable<SelectListItem> CtegoryList { get; set; }
        public IEnumerable<SelectListItem> FrequencyList { get; set; }
    }
}
