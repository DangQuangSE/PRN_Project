using FManagement.Entities.QuangND.Entities;
using FManagement.Repositories.QuangND;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FManagement.Services.QuangND
{
    public class SystemAccountService
    {
        private readonly SystemAccountRepository _systemAccountRepository;
        public SystemAccountService() => _systemAccountRepository ??= new SystemAccountRepository();
        public async Task<SystemUserAccount> GetByUserAccountAsync(string username, string password)
        {
            try
            {
                return await _systemAccountRepository.GetByUserAccountAsync(username, password);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
