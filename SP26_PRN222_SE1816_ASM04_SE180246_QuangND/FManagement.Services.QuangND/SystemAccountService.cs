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
        private SystemUserAccount? _currentUser;

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

        // Authentication methods
        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                _currentUser = await _systemAccountRepository.GetByUserAccountAsync(username, password);
                return _currentUser != null && _currentUser.UserAccountId > 0;
            }
            catch
            {
                return false;
            }
        }

        public Task LogoutAsync()
        {
            _currentUser = null;
            return Task.CompletedTask;
        }

        public int? GetCurrentUserId() => _currentUser?.UserAccountId;
        public string? GetCurrentUserName() => _currentUser?.UserName;
    }
}
