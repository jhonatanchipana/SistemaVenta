using Microsoft.AspNetCore.Identity;
using Venta.Data.Interfaces;
using Venta.Dto.Object.Account;

namespace Venta.Services.Bussiness
{
    public class UsersStore : IUserStore<UserDTO>, IUserEmailStore<UserDTO>, IUserPasswordStore<UserDTO>
    {
        private readonly IUserRepository _userRepository;

        public UsersStore(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<IdentityResult> CreateAsync(UserDTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(UserDTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<UserDTO?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetByEmailNormalized(normalizedEmail);

            if (entity is null) throw new Exception("El usuario no ha sido encontrado");

            try
            {
                var user = new UserDTO()
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    NormalizedName = entity.NormalizedName,
                    Email = entity.Email,
                    EmailNormalizado = entity.NormalizedEmail,
                    PasswordHash = entity.PasswordHash
                };

                return user;
            }
            catch (Exception)
            {
                throw new Exception("Error al buscar usuario por Email");
            }
        }

        public Task<UserDTO?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetByUserNameNormalized(normalizedUserName);

            if (entity is null) throw new Exception("El usuario no ha sido encontrado");

            try
            {
                var user = new UserDTO()
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    NormalizedName = entity.NormalizedName,
                    Email = entity.Email,
                    EmailNormalizado = entity.NormalizedEmail,
                    PasswordHash = entity.PasswordHash
                };

                return user;
            }
            catch (Exception)
            {
                throw new Exception("Error al buscar usuario por el nombre de usuario");
            }
        }

        public Task<string?> GetEmailAsync(UserDTO user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email ?? null);
        }

        public Task<bool> GetEmailConfirmedAsync(UserDTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedEmailAsync(UserDTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedUserNameAsync(UserDTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetPasswordHashAsync(UserDTO user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash ?? null);
        }

        public Task<string> GetUserIdAsync(UserDTO user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string?> GetUserNameAsync(UserDTO user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName ?? null);
        }

        public Task<bool> HasPasswordAsync(UserDTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(UserDTO user, string? email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(UserDTO user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(UserDTO user, string? normalizedEmail, CancellationToken cancellationToken)
        {
            user.EmailNormalizado = normalizedEmail ?? string.Empty;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(UserDTO user, string? normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(UserDTO user, string? passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash ?? string.Empty;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(UserDTO user, string? userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(UserDTO user, CancellationToken cancellationToken)
        {
            return IdentityResult.Success;
        }
    }
}
