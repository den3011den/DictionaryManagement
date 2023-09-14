using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_Models.IntDBModels;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository
{
    public interface IAuthorizationRepository
    {        
        public Task<bool> CurrentUserIsInAdminRole(MessageBoxMode messageBoxModePar = MessageBoxMode.Off);
        public Task<UserDTO>? GetCurrentUserDTO(MessageBoxMode messageBoxModePar = MessageBoxMode.Off);
        public Task<string> GetCurrentLogin(MessageBoxMode messageBoxModePar = MessageBoxMode.Off);
    }
}
