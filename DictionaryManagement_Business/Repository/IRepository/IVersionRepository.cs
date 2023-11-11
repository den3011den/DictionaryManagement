using DictionaryManagement_Common;
using DictionaryManagement_Models.IntDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository.IRepository
{
    public interface IVersionRepository
    {
        public Task<VersionDTO> Get();
        public Task<VersionDTO> Set(VersionDTO objectToAddDTO);
    }
}
