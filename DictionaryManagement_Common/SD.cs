using DictionaryManagement_Models.IntDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_Common
{
    public static class SD
    {
        public enum SelectDictionaryScope
        {
            All,
            ArchiveOnly,
            NotArchiveOnly
        }

        public enum UpdateMode
        {
            Update,
            MoveToArchive,
            RestoreFromArchive
        }

        public enum FactoryMode
        {
            NKNH,
            KOS
        }

        public static string AppVersion = "";
        public static FactoryMode AppFactoryMode = FactoryMode.KOS;
        public static string AdminRoleName = "Администратор";

        public static UserDTO CurrentUserDTO = new UserDTO
        {
            Id = new Guid("A8281E55-524B-4E5E-911A-A8949B011372"),
            Login = "sibur.local\\User1",
            UserName = "Пользователь 1",
            Description = "Это Пользователь 1 с логином sibur.local\\User1",
            IsArchive = false
        };
    }
}
