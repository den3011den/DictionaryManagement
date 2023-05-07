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
    public interface IMesUnitOfMeasureRepository
    {
        public Task<MesUnitOfMeasureDTO> Get(int Id);
        public Task<IEnumerable<MesUnitOfMeasureDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);
        public Task<MesUnitOfMeasureDTO> Update(MesUnitOfMeasureDTO objDTO, UpdateMode updateMode = UpdateMode.Update);
        public Task<MesUnitOfMeasureDTO> Create(MesUnitOfMeasureDTO objectToAddDTO);

    }
}
