using FManagement.Entities.QuangND.Entities;
using FManagement.Repositories.QuangND.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FManagement.Repositories.QuangND
{
    public class SystemAccountRepository : GenericRepository<SystemUserAccount>
    {
        public SystemAccountRepository() { }
        public SystemAccountRepository(FranchiseManagementContext context) => _context = context;
        public async Task<SystemUserAccount> GetByUserAccountAsync(string username, string password)
        {
            return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password && u.IsActive)?? new SystemUserAccount();
        }
    }
}
