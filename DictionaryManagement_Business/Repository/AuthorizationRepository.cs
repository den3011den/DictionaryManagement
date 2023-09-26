using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System.Security.Claims;
using static DictionaryManagement_Business.Repository.IAuthorizationRepository;
using static DictionaryManagement_Common.SD;

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

        public AuthorizationRepository(AuthenticationStateProvider authenticationStateProvider, IUserToRoleRepository userToRoleRepository,
            IUserRepository userRepository, IJSRuntime jsRuntime,
            IADGroupRepository adGroupRepository, IRoleToADGroupRepository roleToADGroupRepository)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _userToRoleRepository = userToRoleRepository;
            _userRepository = userRepository;
            _jsRuntime = jsRuntime;
            _adGroupRepository = adGroupRepository;
            _roleToADGroupRepository = roleToADGroupRepository;
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

        public async Task<int> AddUserToRolesByLoginAndADGroup(AuthenticationState authStatePar)
        {
            string userLogin = authStatePar.User.Identity.Name;

            int userToRoleAddCount = 0;

            if (!userLogin.IsNullOrEmpty())
            {
                IEnumerable<RoleToADGroupDTO> RoleToADGroupDTOList = await _roleToADGroupRepository.GetAll();
                UserDTO userDTO = null;
                UserToRoleDTO userToRoleDTO = null;
                

                foreach (var item in RoleToADGroupDTOList)
                {
                    if (item.ADGroupDTOFK.Name == "S-1-1-0")
                    {
                                               
                        var ggg = authStatePar.User.Claims.ToList();
                        var fff = authStatePar.User.HasClaim(ClaimTypes.Role, item.ADGroupDTOFK.Name.Trim());

                        int a = 1;
                    }

                    

                    if (authStatePar.User.IsInRole(item.ADGroupDTOFK.Name.Trim()))
                    {
                        if (userDTO == null)
                        {
                            userDTO = await _userRepository.GetByLogin(userLogin);
                            if (userDTO == null)
                            {
                                UserDTO userToAddDTO = new UserDTO
                                {
                                    UserName = authStatePar.User.Identity.Name,
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
