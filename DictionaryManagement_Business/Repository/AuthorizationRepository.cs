using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System.Security.Claims;
using static DictionaryManagement_Common.SD;
using Microsoft.Extensions.Configuration;
using System.DirectoryServices.AccountManagement;
using Microsoft.AspNetCore.Authentication;

namespace DictionaryManagement_Business.Repository
{
    public class AuthorizationRepository : IAuthorizationRepository
    {

        private readonly AuthenticationStateProvider _authenticationStateProvider;

        private readonly IUserToRoleRepository _userToRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IADGroupRepository _adGroupRepository;
        private readonly IRoleToADGroupRepository _roleToADGroupRepository;
        private readonly IJSRuntime _jsRuntime;
        private readonly IConfiguration _config;

        public AuthorizationRepository(AuthenticationStateProvider authenticationStateProvider, IUserToRoleRepository userToRoleRepository,
            IUserRepository userRepository, IJSRuntime jsRuntime,
            IADGroupRepository adGroupRepository, IRoleToADGroupRepository roleToADGroupRepository,
            IConfiguration config)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _userToRoleRepository = userToRoleRepository;
            _userRepository = userRepository;
            _jsRuntime = jsRuntime;
            _adGroupRepository = adGroupRepository;
            _roleToADGroupRepository = roleToADGroupRepository;
            _config = config;
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
                        await AddUserToRolesByLoginAndADGroup(authState);
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

        public async Task<string> GetCurrentUser(MessageBoxMode messageBoxModePar = MessageBoxMode.Off, LoginReturnMode loginReturnMode = LoginReturnMode.LoginOnly)
        {

            string? returnString = "";

            if (_authenticationStateProvider is not null)
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                if (authState.User != null)
                {
                    if (authState.User.Identity is not null && authState.User.Identity.IsAuthenticated)
                    {

                        string? loginStr = authState.User.Identity.Name;

                        if (loginReturnMode == LoginReturnMode.LoginOnly)
                            returnString = loginStr;
                        else
                        {                            
                            if (OperatingSystem.IsWindows())
                            {
                                try
                                {
                                    //#pragma warning disable CA1416 // Validate platform compatibility
                                    var context = new PrincipalContext(ContextType.Domain);
                                    var principal = UserPrincipal.FindByIdentity(context, authState.User.Identity.Name);
                                    //#pragma warning restore CA1416 // Validate platform compatibility

                                    var varString = principal.Name;
                                    
                                    if (varString.IsNullOrEmpty())
                                        varString = principal.DisplayName;
                                    if (varString.IsNullOrEmpty())
                                        varString = principal.Surname + " " + principal.GivenName + " ";

                                    if (loginReturnMode == LoginReturnMode.NameOnly)
                                    {                                        
                                        returnString = varString;
                                    }    
                                    if (loginReturnMode == LoginReturnMode.LoginAndName)
                                    {
                                        returnString = varString + " ( " + loginStr + " )";
                                    }

                                }
                                catch (Exception ex)
                                {
                                    returnString = loginStr;
                                }
                            }
                            else
                            {
                                returnString = loginStr;
                            }
                        }
                    }
                }
            }

            if (returnString.IsNullOrEmpty() && messageBoxModePar == MessageBoxMode.On)
            {
                await _jsRuntime.InvokeVoidAsync("ShowSwal", "error", "Не удалось получить логин текущего пользователя.");
            }


            return returnString;
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

        public async Task<int> AddUserToRolesByLoginAndADGroup(AuthenticationState authStatePar)
        {
            string userLogin = await GetCurrentUser(SD.MessageBoxMode.Off, SD.LoginReturnMode.LoginOnly);
            
            int userToRoleAddCount = 0;

            if (!userLogin.IsNullOrEmpty())
            {
                IEnumerable<RoleToADGroupDTO> RoleToADGroupDTOList = await _roleToADGroupRepository.GetAll();
                UserDTO userDTO = null;
                UserToRoleDTO userToRoleDTO = null;
                

                foreach (var item in RoleToADGroupDTOList)
                {
                   
                    if (authStatePar.User.IsInRole(item.ADGroupDTOFK.Name.Trim()))
                    {
                        if (userDTO == null)
                        {
                            userDTO = await _userRepository.GetByLogin(userLogin);
                            if (userDTO == null)
                            {
                                UserDTO userToAddDTO = new UserDTO
                                {
                                    UserName = await GetCurrentUser(SD.MessageBoxMode.Off, SD.LoginReturnMode.NameOnly),
                                    Login = userLogin,
                                    Description = "Добавлено автоматически " + DateTime.Now.ToString(),
                                    IsArchive = false
                                };
                                userDTO = await _userRepository.Create(userToAddDTO);
                                userToRoleAddCount++;
                            }
                        }

                        if (userDTO != null)
                        {
                            if ((!userDTO.IsArchive) && (!item.RoleDTOFK.IsArchive))
                            {
                                {
                                    userToRoleDTO = await _userToRoleRepository.Get(userDTO.Id, item.RoleId);

                                    if (userToRoleDTO == null)
                                    {
                                        UserToRoleDTO userToRoleAddDTO = new UserToRoleDTO
                                        {
                                            UserId = userDTO.Id,
                                            RoleId = item.RoleId,
                                            UserDTOFK = userDTO,
                                            RoleDTOFK = item.RoleDTOFK
                                        };
                                        _userToRoleRepository.Create(userToRoleAddDTO);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return userToRoleAddCount;
        }
    }
}
