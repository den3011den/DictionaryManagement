using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository.IRepository
{
    public interface IMesParamRepository
    {
        public Task<MesParamDTO> GetById(int mesParamId);
        public Task<IEnumerable<MesParamDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);        
        public Task<MesParamDTO> Update(MesParamDTO objDTO);
        public Task<MesParamDTO> Create(MesParamDTO objectToAddDTO);
        public Task<int> Delete(int mesParamId, UpdateMode updateMode = UpdateMode.Update);
        public Task<MesParamDTO> GetByCode(string code = "");
        public Task<MesParamDTO> GetByName(string name = "");
        public Task<MesParamDTO> GetByMesParamSourceLink(string mesParamSourceLink = "");
        public Task<MesParamDTO> GetBySapMapping(int? sapEquipmentIdSource, int? sapEquipmentIdDest, int? sapMaterialId, int idForExclude);
    }
}
