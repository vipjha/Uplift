using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface IOrderHeaderRespository : IRepository<OrderHeader>
    {
        void ChangeOrderStatus(int orderHeaderId, string status);
    }
}
