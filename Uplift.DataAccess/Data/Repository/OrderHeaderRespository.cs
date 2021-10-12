using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class OrderHeaderRespository : Repository<OrderHeader>, IOrderHeaderRespository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRespository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void ChangeOrderStatus(int orderHeaderId, string status)
        {
            var orderFromDb = _db.OrderHeader.FirstOrDefault(o => o.Id == orderHeaderId);
            orderFromDb.Status = status;
            _db.SaveChanges();
        }

    }
}
