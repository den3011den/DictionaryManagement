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
    public interface IUnitOfMeasureSapToMesMappingRepository
    {
        public Task<UnitOfMeasureSapToMesMappingDTO> Get(int sapUnitId, int mesUnitId);
        public Task<UnitOfMeasureSapToMesMappingDTO> GetById(int id);
        public Task<IEnumerable<UnitOfMeasureSapToMesMappingDTO>> GetAll();
        public Task<UnitOfMeasureSapToMesMappingDTO> Update(UnitOfMeasureSapToMesMappingDTO objDTO);
        public Task<UnitOfMeasureSapToMesMappingDTO> Create(UnitOfMeasureSapToMesMappingDTO objectToAddDTO);
        public Task<int> Delete(int Id);
    }
}
