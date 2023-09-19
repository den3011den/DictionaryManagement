using AutoMapper;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;
using DND.EFCoreWithNoLock.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DictionaryManagement_Common.SD;

namespace DictionaryManagement_Business.Repository
{
    public class RoleVMRepository : IRoleVMRepository
    {
        private readonly IntDBApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserToRoleRepository _userToRoleRepository;
        private readonly IReportTemplateTypeTоRoleRepository _reportTemplateTypeTоRoleRepository;

        public RoleVMRepository(IntDBApplicationDbContext db, IMapper mapper
            ,IUserToRoleRepository userToRoleRepository, IReportTemplateTypeTоRoleRepository reportTemplateTypeTоRoleRepository)
        {
            _db = db;
            _mapper = mapper;
            _userToRoleRepository = userToRoleRepository;
            _reportTemplateTypeTоRoleRepository = reportTemplateTypeTоRoleRepository;
        }

        public async Task<RoleVMDTO?> Create(RoleVMDTO objectToAddDTO)
        {
            var objectToAdd = _mapper.Map<RoleVMDTO, Role>(objectToAddDTO);
            var addedRole = _db.Role.Add(objectToAdd);
            _db.SaveChanges();
            return _mapper.Map<Role, RoleVMDTO>(addedRole.Entity);
        }

        public async Task<RoleVMDTO?> Get(Guid Id)
        {

            RoleVMDTO RoleVMDTOToReturn = null;
            if (Id != null && Id != Guid.Empty)
            {
                var objToGet = _db.Role.FirstOrDefaultWithNoLock(u => (u.Id == Id));
                if (objToGet != null)
                {
                    var objUserToRole = _db.UserToRole.Where(u => u.RoleId == Id).Include("UserFK").Include("RoleFK").
                            OrderBy(u => u.UserFK.UserName).ToListWithNoLock();
                    var objReportTemplateTypeToRole = _db.ReportTemplateTypeTоRole.Where(u => u.RoleId == Id).Include("ReportTemplateTypeFK").Include("RoleFK")
                        .OrderBy(u => u.ReportTemplateTypeFK.Name).ToListWithNoLock();

                    RoleVMDTOToReturn = _mapper.Map<Role, RoleVMDTO>(objToGet);
                    if (objUserToRole != null)
                    {
                        RoleVMDTOToReturn.UserToRoleDTOs = _mapper.Map<IEnumerable<UserToRole>, IEnumerable<UserToRoleDTO>>(objUserToRole);
                    }

                    if (objReportTemplateTypeToRole != null)
                    {
                        RoleVMDTOToReturn.ReportTemplateTypeTоRoleDTOs = _mapper.Map<IEnumerable<ReportTemplateTypeTоRole>, IEnumerable<ReportTemplateTypeTоRoleDTO>>(objReportTemplateTypeToRole);
                    }

                }
            }
            return RoleVMDTOToReturn;
        }

        public async Task<IEnumerable<RoleVMDTO>> GetAll(SD.SelectDictionaryScope selectDictionaryScope = SD.SelectDictionaryScope.All)
        {
            IEnumerable<RoleDTO> roleListDTOs = null;
            if (selectDictionaryScope == SD.SelectDictionaryScope.All)
                roleListDTOs = _mapper.Map<IEnumerable<Role>, IEnumerable<RoleDTO>>(_db.Role.ToListWithNoLock());
            if (selectDictionaryScope == SD.SelectDictionaryScope.ArchiveOnly)
                roleListDTOs = _mapper.Map<IEnumerable<Role>, IEnumerable<RoleDTO>>(_db.Role.Where(u => u.IsArchive == true).ToListWithNoLock());
            if (selectDictionaryScope == SD.SelectDictionaryScope.NotArchiveOnly)
                roleListDTOs = _mapper.Map<IEnumerable<Role>, IEnumerable<RoleDTO>>(_db.Role.Where(u => u.IsArchive != true).ToListWithNoLock());

            List<RoleVMDTO> roleVMDTOs = new();
            foreach (var roleDTO in roleListDTOs)
            {

                var roleId = roleDTO.Id;
                var objUserToRole = _db.UserToRole.Where(u => u.RoleId == roleId).Include("UserFK").Include("RoleFK").
                    OrderBy(u => u.UserFK.UserName).ToListWithNoLock();
                var objReportTemplateTypeToRole = _db.ReportTemplateTypeTоRole.Where(u => u.RoleId == roleId).Include("ReportTemplateTypeFK").Include("RoleFK")
                    .OrderBy(u => u.ReportTemplateTypeFK.Name).ToListWithNoLock();

                var addRoleVMDTO = _mapper.Map<RoleDTO, RoleVMDTO>(roleDTO);
                if (objUserToRole != null)
                {
                    addRoleVMDTO.UserToRoleDTOs = _mapper.Map<IEnumerable<UserToRole>, IEnumerable<UserToRoleDTO>>(objUserToRole);
                }

                if (objReportTemplateTypeToRole != null)
                {
                    addRoleVMDTO.ReportTemplateTypeTоRoleDTOs = _mapper.Map<IEnumerable<ReportTemplateTypeTоRole>, IEnumerable<ReportTemplateTypeTоRoleDTO>>(objReportTemplateTypeToRole);
                }

                if (addRoleVMDTO != null) {
                    roleVMDTOs.Add(addRoleVMDTO);
                }
            }
            return roleVMDTOs;
        }

        public async Task<RoleVMDTO?> GetByName(string name)
        {

            RoleVMDTO RoleVMDTOToReturn = null;
            if (name.IsNullOrEmpty())
            {
                var objToGet = _db.Role.FirstOrDefaultWithNoLock(u => (u.Name.Trim().ToUpper() == name.Trim().ToUpper()));
                if (objToGet != null)
                {

                    var roleId = objToGet.Id;
                    var objUserToRole = _db.UserToRole.Where(u => u.RoleId == roleId).Include("UserFK").Include("RoleFK").
                            OrderBy(u => u.UserFK.UserName).ToListWithNoLock();
                    var objReportTemplateTypeToRole = _db.ReportTemplateTypeTоRole.Where(u => u.RoleId == roleId).Include("ReportTemplateTypeFK").Include("RoleFK")
                        .OrderBy(u => u.ReportTemplateTypeFK.Name).ToListWithNoLock();

                    RoleVMDTOToReturn = _mapper.Map<Role, RoleVMDTO>(objToGet);
                    if (objUserToRole != null)
                    {
                        RoleVMDTOToReturn.UserToRoleDTOs = _mapper.Map<IEnumerable<UserToRole>, IEnumerable<UserToRoleDTO>>(objUserToRole);
                    }

                    if (objReportTemplateTypeToRole != null)
                    {
                        RoleVMDTOToReturn.ReportTemplateTypeTоRoleDTOs = _mapper.Map<IEnumerable<ReportTemplateTypeTоRole>, IEnumerable<ReportTemplateTypeTоRoleDTO>>(objReportTemplateTypeToRole);

                    }

                }
            }
            return RoleVMDTOToReturn;
        }


        public async Task<RoleVMDTO?> Update(RoleVMDTO objectToUpdateVMDTO, SD.UpdateMode updateMode = SD.UpdateMode.Update)
        {
            var objectToUpdate = _db.Role.FirstOrDefaultWithNoLock(u => u.Id == objectToUpdateVMDTO.Id);
            if (objectToUpdate != null)
            {
                if (updateMode == SD.UpdateMode.Update)
                {
                    if (objectToUpdate.Name != objectToUpdateVMDTO.Name)
                        objectToUpdate.Name = objectToUpdateVMDTO.Name;
                    if (objectToUpdate.Description != objectToUpdateVMDTO.Description)
                        objectToUpdate.Description = objectToUpdateVMDTO.Description;
                }
                if (updateMode == SD.UpdateMode.MoveToArchive)
                {
                    objectToUpdate.IsArchive = true;
                }
                if (updateMode == SD.UpdateMode.RestoreFromArchive)
                {
                    objectToUpdate.IsArchive = false;
                }
                _db.Role.Update(objectToUpdate);
                _db.SaveChanges();

                var retRoleVMDTO = await Get(objectToUpdateVMDTO.Id);
                return retRoleVMDTO;
            }
            return null;
        }

        public async Task<UserToRoleDTO?> AddUserToRole(RoleVMDTO roleVMDTO, UserDTO addUserDTO)
        {
            var checkUserToRole = _db.UserToRole.FirstOrDefaultWithNoLock(u => u.RoleId == roleVMDTO.Id && u.UserId == addUserDTO.Id);
            if (checkUserToRole != null) 
            {
                // уже есть связка роли и пользователя
                return null;
            }

            UserToRole objectUserToRoleToAdd = new UserToRole();

            objectUserToRoleToAdd.UserId = addUserDTO.Id;
            objectUserToRoleToAdd.RoleId = roleVMDTO.Id;

            var addedUserToRole = _db.UserToRole.Add(objectUserToRoleToAdd);
            _db.SaveChanges();

            return _mapper.Map<UserToRole, UserToRoleDTO>(addedUserToRole.Entity);

        }

        public async Task<ReportTemplateTypeTоRoleDTO?> AddReportTemplateTypeToRole(RoleVMDTO roleVMDTO, ReportTemplateTypeDTO addreportTemplateTypeDTO)
        {
            var checkReportTemplateTypeTоRole = _db.ReportTemplateTypeTоRole.FirstOrDefaultWithNoLock(u => u.RoleId == roleVMDTO.Id && u.ReportTemplateTypeId == addreportTemplateTypeDTO.Id);
            if (checkReportTemplateTypeTоRole != null)
            {
                // уже есть связка роли и типа шаблона отчёта
                return null;
            }

            ReportTemplateTypeTоRole objectReportTemplateTypeTоRoleToAdd = new ReportTemplateTypeTоRole();

            objectReportTemplateTypeTоRoleToAdd.ReportTemplateTypeId = addreportTemplateTypeDTO.Id;
            objectReportTemplateTypeTоRoleToAdd.RoleId = roleVMDTO.Id;

            var addedReportTemplateTypeTоRole = _db.ReportTemplateTypeTоRole.Add(objectReportTemplateTypeTоRoleToAdd);
            _db.SaveChanges();

            return _mapper.Map<ReportTemplateTypeTоRole, ReportTemplateTypeTоRoleDTO>(addedReportTemplateTypeTоRole.Entity);

        }

        public async Task<int> DeleteUserToRole(int userToRoleId)
        {
            if (userToRoleId > 0)
            {
                return await _userToRoleRepository.Delete(userToRoleId);
            }
            return 0;
        }

        public async Task<int> DeleteReportTemplateTypeToRole(int reportTemplateTypeToRoleId)
        {
            if (reportTemplateTypeToRoleId > 0)
            {
                return await _reportTemplateTypeTоRoleRepository.Delete(reportTemplateTypeToRoleId);
            }
            return 0;
        }
    }
}
