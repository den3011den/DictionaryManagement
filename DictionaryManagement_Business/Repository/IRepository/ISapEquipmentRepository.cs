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
    public interface ISapEquipmentRepository
    {
        public Task<SapEquipmentDTO> Get(int Id);
        public Task<IEnumerable<SapEquipmentDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All);
        public Task<SapEquipmentDTO> Update(SapEquipmentDTO objDTO, UpdateMode updateMode = UpdateMode.Update);
        public Task<SapEquipmentDTO> Create(SapEquipmentDTO objectToAddDTO);      
    }
}
