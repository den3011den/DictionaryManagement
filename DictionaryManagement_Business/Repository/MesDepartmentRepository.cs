using AutoMapper;
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


        public async Task<IEnumerable<MesDepartmentDTO>> GetAll()
        {
                var hhh1 = _db.MesDepartment.Include("DepartmentParent");
                return _mapper.Map<IEnumerable<MesDepartment>, IEnumerable<MesDepartmentDTO>>(hhh1);
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
            var hhh = _db.MesDepartment.Where(u => u.ParentDepartmentId == id).Any();
            return hhh;
        }

        public async Task<MesDepartmentDTO> Update(MesDepartmentDTO objectToUpdateDTO)
        {
            var objectToUpdate = _db.MesDepartment.Include("DepartmentParent").
                    FirstOrDefault(u => u.Id == objectToUpdateDTO.Id);
            if (objectToUpdate != null)
            {
                if (objectToUpdate.ParentDepartmentId != objectToUpdateDTO.DepartmentParentDTO.Id)
                {
                    objectToUpdate.ParentDepartmentId = objectToUpdateDTO.DepartmentParentDTO.Id;
                    objectToUpdate.DepartmentParent = _mapper.Map<MesDepartmentDTO, MesDepartment>(objectToUpdateDTO.DepartmentParentDTO);
                }
                _db.MesDepartment.Update(objectToUpdate);
                await _db.SaveChangesAsync();
                return _mapper.Map<MesDepartment, MesDepartmentDTO>(objectToUpdate);
            }
            return objectToUpdateDTO;

        }

        public async Task<int> Delete(int id)
        {
            if (id > 0)
            {
                var objectToDelete = _db.MesDepartment.FirstOrDefault(u => u.Id == id);
                if (objectToDelete != null)
                {
                    _db.MesDepartment.Remove(objectToDelete);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;

        }
    }
}
