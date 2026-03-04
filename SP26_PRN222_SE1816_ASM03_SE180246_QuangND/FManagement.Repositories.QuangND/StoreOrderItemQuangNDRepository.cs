using FManagement.Entities.QuangND.Entities;
using FManagement.Repositories.QuangND.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FManagement.Repositories.QuangND
{
    public class StoreOrderItemQuangNDRepository : GenericRepository<StoreOrderItemQuangNd>
    {
        public StoreOrderItemQuangNDRepository() { }
        public StoreOrderItemQuangNDRepository(FranchiseManagementContext context) => _context = context;
    }
}
