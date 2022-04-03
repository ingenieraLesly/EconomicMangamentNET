using EconomicManagementAPP.Interfaces;
using EconomicManagementAPP.Models;
using Microsoft.AspNetCore.Identity;

namespace EconomicManagementAPP.Services
{
    public class UserStore : IUserStore<Users>, IUserEmailStore<Users>, IUserPasswordStore<Users>
    {
        private readonly IRepositorieUsers repositorieUsers;

        public UserStore(IRepositorieUsers repositorieUsers)
        {
            this.repositorieUsers = repositorieUsers;
        }

        public async Task<IdentityResult> CreateAsync(Users user, CancellationToken cancellationToken)
        {
            await repositorieUsers.Create(user);
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<Users> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await repositorieUsers.GetUserByEmail(normalizedEmail);
        }

        public Task<Users> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Users> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await repositorieUsers.GetUserByEmail(normalizedUserName);
        }

        public Task<string> GetEmailAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<string> GetUserIdAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> HasPasswordAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(Users user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(Users user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(Users user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.StandarEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(Users user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(Users user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Users user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
