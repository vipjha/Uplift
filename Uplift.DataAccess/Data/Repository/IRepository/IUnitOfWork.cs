using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IFrequencyRepository Frequency { get; }
        IServiceRepository Service { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IOrderHeaderRespository OrderHeader { get; }

        IUserRepository User { get; }

        void Save();
    }
}
