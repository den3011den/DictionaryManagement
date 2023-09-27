using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using Microsoft.AspNetCore.Components.Authorization;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository
{
    public interface IAuthorizationRepository
    {        
        public Task<bool> CurrentUserIsInAdminRole(MessageBoxMode messageBoxModePar = MessageBoxMode.Off);
        public Task<UserDTO>? GetCurrentUserDTO(MessageBoxMode messageBoxModePar = MessageBoxMode.Off);
        public Task<string> GetCurrentUser(MessageBoxMode messageBoxModePar = MessageBoxMode.Off, LoginReturnMode loginReturnMode = LoginReturnMode.LoginOnly);
        public Task<bool> CurrentUserIsInAdminRoleByLogin(string userLogin, MessageBoxMode messageBoxModePar = MessageBoxMode.Off);
        public Task<int> AddUserToRolesByLoginAndADGroup(AuthenticationState authStatePar);
    }
}
