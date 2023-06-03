﻿using AutoMapper;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository
{
    public class MesDepartmentRepository : IMesDepartmentRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;

        public MesDepartmentRepository(IntDBApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<MesDepartmentDTO> Create(MesDepartmentDTO objectToAddDTO)
        {
            //var objectToAdd = _mapper.Map<UnitOfMeasureSapToMesMappingDTO, UnitOfMeasureSapToMesMapping>(objectToAddDTO);

            MesDepartment objectToAdd = new MesDepartment();

            objectToAdd.Id = objectToAddDTO.Id;
            objectToAdd.MesCode = objectToAddDTO.MesCode;
            objectToAdd.Name = objectToAddDTO.Name;
            objectToAdd.ShortName = objectToAddDTO.ShortName;
            objectToAdd.ParentDepartmentId = objectToAddDTO.ParentDepartmentId;
            objectToAdd.IsArchive = objectToAddDTO.IsArchive;

            var addedMesDepartment = _db.MesDepartment.Add(objectToAdd);
            await _db.SaveChangesAsync();
            return _mapper.Map<MesDepartment, MesDepartmentDTO>(addedMesDepartment.Entity);
        }


        public async Task<MesDepartmentDTO> GetById(int id)
        {
            var objToGet = _db.MesDepartment.Include("DepartmentParent").
                            FirstOrDefaultAsync(u => u.Id == id).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<MesDepartment, MesDepartmentDTO>(objToGet);
            }
            return null;
        }


        public async Task<IEnumerable<MesDepartmentDTO>> GetAll(SelectDictionaryScope selectDictionaryScope = SelectDictionaryScope.All)
        {
            if (selectDictionaryScope == SD.SelectDictionaryScope.All)
            {
                var hhh1 = _db.MesDepartment.Include("DepartmentParent");
                return _mapper.Map<IEnumerable<MesDepartment>, IEnumerable<MesDepartmentDTO>>(hhh1);
            }
            if (selectDictionaryScope == SD.SelectDictionaryScope.ArchiveOnly)
            {
                var hhh2 = _db.MesDepartment.Include("DepartmentParent").Where(u => u.IsArchive == true);
                return _mapper.Map<IEnumerable<MesDepartment>, IEnumerable<MesDepartmentDTO>>(hhh2);
            }            
            if (selectDictionaryScope == SD.SelectDictionaryScope.NotArchiveOnly)
            {
                var hhh3 = _db.MesDepartment.Include("DepartmentParent").Where(u => u.IsArchive != true);
                return _mapper.Map<IEnumerable<MesDepartment>, IEnumerable<MesDepartmentDTO>>(hhh3);

            }
            var hhh4 = _db.MesDepartment.Include("DepartmentParent");
            return _mapper.Map<IEnumerable<MesDepartment>, IEnumerable<MesDepartmentDTO>>(hhh4);
        }

        public async Task<IEnumerable<MesDepartmentDTO>> GetAllTopLevel()
        {
                var hhh1 = _db.MesDepartment.Include("DepartmentParent").Where(u => (u.ParentDepartmentId == null || u.ParentDepartmentId <= 0));
                return _mapper.Map<IEnumerable<MesDepartment>, IEnumerable<MesDepartmentDTO>>(hhh1);
        }

        public async Task<IEnumerable<MesDepartmentDTO>> GetChildList(int id)
        {
                var hhh1 = _db.MesDepartment.Include("DepartmentParent").Where(u => u.ParentDepartmentId == id);
                return _mapper.Map<IEnumerable<MesDepartment>, IEnumerable<MesDepartmentDTO>>(hhh1);
        }

        public async Task<bool> HasChild(int id)        
        {
            var hhh5 = _db.MesDepartment.Where(u => u.ParentDepartmentId == id).AsNoTracking().Any();
            return hhh5;            
        }

        public async Task<MesDepartmentDTO> Update(MesDepartmentDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.MesDepartment.Include("DepartmentParent").
                    FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {
                if (objectToUpdateDTO.ParentDepartmentId == null)
                {
                    objectToUpdate.ParentDepartmentId = null;
                    objectToUpdate.DepartmentParent = null;
                }    
                else
                {
                    if (objectToUpdate.ParentDepartmentId != objectToUpdateDTO.ParentDepartmentId)
                    {
                        objectToUpdate.ParentDepartmentId = objectToUpdateDTO.ParentDepartmentId;
                        var objectParentToUpdate = _db.MesDepartment.Include("DepartmentParent").
                                FirstOrDefault(u => u.Id == objectToUpdateDTO.ParentDepartmentId);
                        objectToUpdate.DepartmentParent = objectParentToUpdate;
                    }
                }
                if (objectToUpdateDTO.Name != objectToUpdate.Name)
                    objectToUpdate.Name = objectToUpdateDTO.Name;
                if (objectToUpdateDTO.ShortName != objectToUpdate.ShortName)
                    objectToUpdate.ShortName = objectToUpdateDTO.ShortName;
                if (objectToUpdateDTO.MesCode != objectToUpdate.MesCode)
                    objectToUpdate.MesCode = objectToUpdateDTO.MesCode;

                _db.MesDepartment.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<MesDepartment, MesDepartmentDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(int id, UpdateMode updateMode = UpdateMode.Update)
        {
            if (id > 0)
            {
                var objectToDelete = _db.MesDepartment.FirstOrDefault(u => u.Id == id);
                if (objectToDelete != null)
                {
                    if (updateMode == SD.UpdateMode.MoveToArchive)
                        objectToDelete.IsArchive = true;
                    if (updateMode == SD.UpdateMode.RestoreFromArchive)
                        objectToDelete.IsArchive = false;                    
                    _db.MesDepartment.Update(objectToDelete);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;

        }

        public async Task<MesDepartmentDTO> GetByCode(int mesCode = 0)
        {
            var objToGet = _db.MesDepartment.FirstOrDefaultAsync(u => u.MesCode == mesCode).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<MesDepartment, MesDepartmentDTO>(objToGet);
            }
            return null;
        }

        public async Task<MesDepartmentDTO> GetByName(string name = "")
        {
            var objToGet = _db.MesDepartment.FirstOrDefaultAsync(u => u.Name.Trim().ToUpper() == name.Trim().ToUpper()).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<MesDepartment, MesDepartmentDTO>(objToGet);
            }
            return null;
        }

        public async Task<MesDepartmentDTO> GetByShortName(string shortName = "")
        {
            var objToGet = _db.MesDepartment.FirstOrDefaultAsync(u => u.ShortName.Trim().ToUpper() == shortName.Trim().ToUpper()).GetAwaiter().GetResult();
            if (objToGet != null)
            {
                return _mapper.Map<MesDepartment, MesDepartmentDTO>(objToGet);
            }
            return null;
        }
    }
}
