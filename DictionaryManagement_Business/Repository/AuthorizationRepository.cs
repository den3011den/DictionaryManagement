using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_Models.IntDBModels;
using Microsoft.AspNetCore.Components.Authorization;

namespace DictionaryManagement_Business.Repository
{
    public class AuthorizationRepository:IAuthorizationRepository
    {

        private readonly AuthenticationStateProvider _authenticationStateProvider;

        private readonly IUserToRoleRepository _userToRoleRepository;
        private readonly IUserRepository _userRepository;

        public AuthorizationRepository(AuthenticationStateProvider authenticationStateProvider, IUserToRoleRepository userToRoleRepository, IUserRepository userRepository)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _userToRoleRepository = userToRoleRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CurrentUserIsInRole()
        {
            if (_authenticationStateProvider is not null)
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                if (authState.User != null)
                {
                    if (authState.User.Identity is not null && authState.User.Identity.IsAuthenticated)
                    {
                        bool retVar = await _userToRoleRepository.IsUserInRoleByUserLoginAndRoleName(authState.User.Identity.Name, SD.AdminRoleName);
                        return retVar;
                    }
                }
            }
            return false;
        }

        public async Task<UserDTO> GetCurrentUserDTO()
        {

            if (_authenticationStateProvider is not null)
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                if (authState.User != null)
                {
                    if (authState.User.Identity is not null && authState.User.Identity.IsAuthenticated)
                    {
                        UserDTO retVar = await _userRepository.GetByLoginNotInArchive(authState.User.Identity.Name);
                        return retVar;
                    }
                }
            }
            return null;
        }

        public async Task<string> GetCurrentLogin()
        {
            if (_authenticationStateProvider is not null)
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                if (authState.User != null)
                {
                    if (authState.User.Identity is not null && authState.User.Identity.IsAuthenticated)
                    {
                        return authState.User.Identity.Name;                        
                    }
                }
            }
            return "";
        }
    }
}
