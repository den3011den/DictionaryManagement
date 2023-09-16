using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository
{
    public interface IAuthorizationControllersRepository
    {        
        public Task<bool> CurrentUserIsInAdminRoleByLogin(string userLogin, MessageBoxMode messageBoxModePar = MessageBoxMode.Off);
    }
}
