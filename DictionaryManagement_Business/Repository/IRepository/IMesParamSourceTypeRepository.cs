﻿using DictionaryManagement_Common;
using DictionaryManagement_Models.IntDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository.IRepository
{
    public interface IMesParamSourceTypeRepository
    {
        public Task<MesParamSourceTypeDTO> Get(int Id);
        public Task<IEnumerable<MesParamSourceTypeDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);
        public Task<MesParamSourceTypeDTO> Update(MesParamSourceTypeDTO objDTO, UpdateMode updateMode = UpdateMode.Update);
        public Task<MesParamSourceTypeDTO> Create(MesParamSourceTypeDTO objectToAddDTO);
    }
}
