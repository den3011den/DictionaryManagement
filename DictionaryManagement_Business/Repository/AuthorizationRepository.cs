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
                        await SyncRolesByLoginWithADGroup(authState);
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

        public async Task SyncRolesByLoginWithADGroup(AuthenticationState authStatePar)
        {
            string userLogin = await GetCurrentUser(SD.MessageBoxMode.Off, SD.LoginReturnMode.LoginOnly);
            string userName = await GetCurrentUser(SD.MessageBoxMode.Off, SD.LoginReturnMode.NameOnly);

            int userToRoleAddCount = 0;

            if (!userLogin.IsNullOrEmpty())
            {
                
                UserDTO userFromDBDTO = await _userRepository.GetByLogin(userLogin);
                UserToRoleDTO userToRoleDTO = null;

                bool needAddUser = false;
                bool needCheckAddGroups = false;
                bool needCheckDeleteGroups = false;
                if (userFromDBDTO != null)
                {
                    needAddUser = false;
                    if (userFromDBDTO.IsArchive != true)
                    {
                        if (userFromDBDTO.IsSyncWithAD == true)
                        {
                            needCheckAddGroups = true;
                            needCheckDeleteGroups = true;
                        }
                        else
                        {
                            needCheckAddGroups = false;
                            needCheckDeleteGroups = false;
                        }
                    }
                    else
                    {
                        needCheckAddGroups = false;
                        needCheckDeleteGroups = false;
                    }
                }
                else
                {
                    needAddUser = true;
                    needCheckAddGroups = true;
                    needCheckDeleteGroups = false;
                }

                if (needAddUser == true)
                {
                    userFromDBDTO = new UserDTO
                    {
                        UserName = await GetCurrentUser(SD.MessageBoxMode.Off, SD.LoginReturnMode.NameOnly),
                        Login = userLogin,
                        Description = "Добавлено автоматически " + DateTime.Now.ToString(),
                        IsArchive = false
                    };
                    userFromDBDTO = await _userRepository.Create(userFromDBDTO);
                }

                IEnumerable<RoleToADGroupDTO> RoleToADGroupDTOList = null;
                if ((needCheckAddGroups == true) || (needCheckDeleteGroups == true))
                    RoleToADGroupDTOList = (await _roleToADGroupRepository.GetAll()).OrderBy(u => u.RoleId);

                if (needCheckAddGroups == true)
                {                    
                    foreach (var item in RoleToADGroupDTOList)
                        {
                            if (item.ADGroupDTOFK.IsArchive != true)
                            {
                                if (authStatePar.User.IsInRole(item.ADGroupDTOFK.Name.Trim()))
                                    {                         
                                        userToRoleDTO = await _userToRoleRepository.Get(userFromDBDTO.Id, item.RoleId);
                                        if (userToRoleDTO == null)
                                            {
                                                UserToRoleDTO userToRoleAddDTO = new UserToRoleDTO
                                                    {
                                                        UserId = userFromDBDTO.Id,
                                                        RoleId = item.RoleId,
                                                        UserDTOFK = userFromDBDTO,
                                                        RoleDTOFK = item.RoleDTOFK
                                                    };
                                                _userToRoleRepository.Create(userToRoleAddDTO);
                                            }
                                    }                                
                            }
                        }
                    }
                if (needCheckDeleteGroups == true)
                {
                    List<Guid> roleIdList = RoleToADGroupDTOList.Select(u => u.RoleId).Distinct().ToList();
                    IEnumerable<RoleToADGroupDTO> roleToADDelDTOList = null;
                    UserToRoleDTO foundUserToRoleDTO = null;
                    bool needDeleteUserInRoleFlag = false;
                    foreach (var roleId in roleIdList)
                    {

                        needDeleteUserInRoleFlag = true;
                        foundUserToRoleDTO = await _userToRoleRepository.Get(userFromDBDTO.Id, roleId);

                        if (foundUserToRoleDTO != null)
                        {
                            roleToADDelDTOList = RoleToADGroupDTOList.Where(u => u.RoleId == roleId && u.ADGroupDTOFK.IsArchive != true);
                            if (roleToADDelDTOList.Count() > 0)
                            {
                                foreach(var varitem in roleToADDelDTOList)
                                {
                                    if (authStatePar.User.IsInRole(varitem.ADGroupDTOFK.Name.Trim()))
                                    {
                                        if (varitem.ADGroupDTOFK.IsArchive != true)
                                        {
                                            needDeleteUserInRoleFlag = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                                needDeleteUserInRoleFlag = true;
                        }
                        else
                        {
                            needDeleteUserInRoleFlag = false;
                        }
                        
                        if(needDeleteUserInRoleFlag)
                        {
                            await _userToRoleRepository.DeleteByRoleId(roleId);
                        }
                    }
                }
            }
        }
    }
}
