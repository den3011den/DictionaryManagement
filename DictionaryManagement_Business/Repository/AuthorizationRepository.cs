using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using static DictionaryManagement_Business.Repository.IAuthorizationRepository;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository
{
    public class AuthorizationRepository : IAuthorizationRepository
    {

        private readonly AuthenticationStateProvider _authenticationStateProvider;

        private readonly IUserToRoleRepository _userToRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJSRuntime _jsRuntime;

        public AuthorizationRepository(AuthenticationStateProvider authenticationStateProvider, IUserToRoleRepository userToRoleRepository, IUserRepository userRepository, IJSRuntime jsRuntime)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _userToRoleRepository = userToRoleRepository;
            _userRepository = userRepository;
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> CurrentUserIsInAdminRole(MessageBoxMode messageBoxModePar = SD.MessageBoxMode.Off)
        {            
            bool retVar = false;
            bool messShownFlag = false;

            if (_authenticationStateProvider is not null)
            {                
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                if (authState.User != null)
                {
                    if (authState.User.Identity is not null && authState.User.Identity.IsAuthenticated)
                    {                        
                        retVar = await _userToRoleRepository.IsUserInRoleByUserLoginAndRoleName(authState.User.Identity.Name, SD.AdminRoleName);                        
                    }
                }

                if (retVar == false)
                {
                    if (messageBoxModePar == MessageBoxMode.On)
                    {
                        messShownFlag = true;
                        await _jsRuntime.InvokeVoidAsync("ShowSwal", "error", "Пользователь " + authState.User.Identity.Name +
                            " не найден, находится в архиве или не имеет роли " + SD.AdminRoleName + ". Обратитесь в техподдержку.");
                    }
                }
            }


            if (retVar == false && messShownFlag == false)
            {
                if (messageBoxModePar == MessageBoxMode.On)
                {
                    messShownFlag = true;
                    await _jsRuntime.InvokeVoidAsync("ShowSwal", "error", "Не удалось получить логин текущего пользователя.");
                }
            }


            return retVar;
        }

        public async Task<UserDTO>? GetCurrentUserDTO(MessageBoxMode messageBoxModePar = MessageBoxMode.Off)
        {

            UserDTO retVar = null;

            bool messShownFlag = false;

            if (_authenticationStateProvider is not null)
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                if (authState.User != null)
                {
                    if (authState.User.Identity is not null && authState.User.Identity.IsAuthenticated)
                    {
                        retVar = await _userRepository.GetByLoginNotInArchive(authState.User.Identity.Name);
                    }
                }

                if (retVar == null)
                {
                    if (messageBoxModePar == MessageBoxMode.On)
                    {
                        messShownFlag = true;
                        await _jsRuntime.InvokeVoidAsync("ShowSwal", "error", "Пользователь " + authState.User.Identity.Name +
                            " не найден в справочнике пользователей СИР");
                    }
                }
            }

            if (messShownFlag == false && retVar == null)
            {
                    if (messageBoxModePar == MessageBoxMode.On)
                    {
                        messShownFlag = true;
                    await _jsRuntime.InvokeVoidAsync("ShowSwal", "error", "Не удалось получить логин текущего пользователя.");
                }

                }
            return retVar;
        }

        public async Task<string> GetCurrentLogin(MessageBoxMode messageBoxModePar = MessageBoxMode.Off)
        {

            string retsLoginString = "";

            if (_authenticationStateProvider is not null)
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                if (authState.User != null)
                {
                    if (authState.User.Identity is not null && authState.User.Identity.IsAuthenticated)
                    {
                        retsLoginString = authState.User.Identity.Name;
                    }
                }
            }

            if (retsLoginString.IsNullOrEmpty() && messageBoxModePar == MessageBoxMode.On)
            {
                await _jsRuntime.InvokeVoidAsync("ShowSwal", "error", "Не удалось получить логин текущего пользователя.");
            }

            return retsLoginString;
        }

        public async Task<bool> CurrentUserIsInAdminRoleByLogin(string userLogin, MessageBoxMode messageBoxModePar = SD.MessageBoxMode.Off)
        {
            bool retVar = false;
            bool messShownFlag = false;

            if (!userLogin.IsNullOrEmpty())
            {
                retVar = await _userToRoleRepository.IsUserInRoleByUserLoginAndRoleName(userLogin, SD.AdminRoleName);
            }

            if (retVar == false)
            {
                if (messageBoxModePar == MessageBoxMode.On)
                {
                    messShownFlag = true;
                    await _jsRuntime.InvokeVoidAsync("ShowSwal", "error", "Пользователь " + userLogin +
                        " не найден, находится в архиве или не имеет роли " + SD.AdminRoleName + ". Обратитесь в техподдержку.");
                }
            }



            if (retVar == false && messShownFlag == false)
            {
                if (messageBoxModePar == MessageBoxMode.On)
                {
                    messShownFlag = true;
                    await _jsRuntime.InvokeVoidAsync("ShowSwal", "error", "Не удалось получить логин текущего пользователя.");
                }
            }


            return retVar;
        }
    }
}
