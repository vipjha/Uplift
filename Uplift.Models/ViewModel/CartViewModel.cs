using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uplift.Models.ViewModel
{
    public class CartViewModel
    {
        public IList<Service> ServicesList { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
