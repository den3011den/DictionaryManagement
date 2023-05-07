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
    public interface ISettingsRepository
    {
        public Task<SettingsDTO> Get(int Id);
        public Task<IEnumerable<SettingsDTO>> GetListByName(string name);
        public Task<IEnumerable<SettingsDTO>> GetAll();
        public Task<SettingsDTO> Update(SettingsDTO objDTO);
        public Task<SettingsDTO> Create(SettingsDTO objectToAddDTO);
        public Task<int> Delete(int Id);
    }
}
