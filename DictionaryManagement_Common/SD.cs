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

    }
}
