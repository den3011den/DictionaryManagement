using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_Models.IntDBModels;

namespace DictionaryManagement_Business.Repository
{
    public interface IAuthorizationRepository
    {        
        public Task<bool> CurrentUserIsInRole();
        public Task<UserDTO> GetCurrentUserDTO();
        public Task<string> GetCurrentLogin();
    }
}
