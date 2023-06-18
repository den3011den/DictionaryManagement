using AutoMapper;
using DictionaryManagement_DataAccess.Data.IntDB;
using DictionaryManagement_Models.IntDBModels;

namespace DictionaryManagement_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SapEquipment, SapEquipmentDTO>().ReverseMap();
            CreateMap<SapMaterial, SapMaterialDTO>().ReverseMap();
            CreateMap<MesMaterial, MesMaterialDTO>().ReverseMap();
            CreateMap<MesUnitOfMeasure, MesUnitOfMeasureDTO>().ReverseMap();
            CreateMap<SapUnitOfMeasure, SapUnitOfMeasureDTO>().ReverseMap();            
            CreateMap<CorrectionReason, CorrectionReasonDTO>().ReverseMap();

            CreateMap<MesParamSourceType, MesParamSourceTypeDTO>().ReverseMap();
            CreateMap<DataType, DataTypeDTO>().ReverseMap();
            CreateMap<DataSource, DataSourceDTO>().ReverseMap();
            CreateMap<ReportTemplateType, ReportTemplateTypeDTO>().ReverseMap();
            CreateMap<LogEventType, LogEventTypeDTO>().ReverseMap();
            CreateMap<Settings, SettingsDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();

            CreateMap<UnitOfMeasureSapToMesMapping, UnitOfMeasureSapToMesMappingDTO>().ReverseMap();

            CreateMap<UnitOfMeasureSapToMesMapping, UnitOfMeasureSapToMesMappingDTO>()
                    .ForMember(dest => dest.SapUnitOfMeasureDTO, opt => opt.MapFrom(src => src.SapUnitOfMeasure))
                    .ForMember(dest => dest.MesUnitOfMeasureDTO, opt => opt.MapFrom(src => src.MesUnitOfMeasure));

            CreateMap<UnitOfMeasureSapToMesMappingDTO, UnitOfMeasureSapToMesMapping>()
                    .ForMember(dest => dest.SapUnitOfMeasure, opt => opt.MapFrom(src => src.SapUnitOfMeasureDTO))
                    .ForMember(dest => dest.MesUnitOfMeasure, opt => opt.MapFrom(src => src.MesUnitOfMeasureDTO));


            CreateMap<SapToMesMaterialMapping, SapToMesMaterialMappingDTO>().ReverseMap();

            CreateMap<SapToMesMaterialMapping, SapToMesMaterialMappingDTO>()
                    .ForMember(dest => dest.SapMaterialDTO, opt => opt.MapFrom(src => src.SapMaterial))
                    .ForMember(dest => dest.MesMaterialDTO, opt => opt.MapFrom(src => src.MesMaterial));

            CreateMap<SapToMesMaterialMappingDTO, SapToMesMaterialMapping>()
                    .ForMember(dest => dest.SapMaterial, opt => opt.MapFrom(src => src.SapMaterialDTO))
                    .ForMember(dest => dest.MesMaterial, opt => opt.MapFrom(src => src.MesMaterialDTO));

            CreateMap<MesDepartmentDTO, MesDepartment>()
                .ForMember(dest => dest.DepartmentParent, opt => opt.MapFrom(src => src.DepartmentParentDTO));
            CreateMap<MesDepartment, MesDepartmentDTO>()
                .ForMember(dest => dest.DepartmentParentDTO, opt => opt.MapFrom(src => src.DepartmentParent));


            CreateMap<MesParamDTO, MesParam>()
                .ForMember(dest => dest.MesParamSourceTypeFK, opt => opt.MapFrom(src => src.MesParamSourceTypeDTOFK))
                .ForMember(dest => dest.MesDepartmentFK, opt => opt.MapFrom(src => src.MesDepartmentDTOFK))
                .ForMember(dest => dest.SapEquipmentSourceFK, opt => opt.MapFrom(src => src.SapEquipmentSourceDTOFK))
                .ForMember(dest => dest.SapEquipmentDestFK, opt => opt.MapFrom(src => src.SapEquipmentDestDTOFK))
                .ForMember(dest => dest.MesMaterialFK, opt => opt.MapFrom(src => src.MesMaterialDTOFK))
                .ForMember(dest => dest.SapMaterialFK, opt => opt.MapFrom(src => src.SapMaterialDTOFK))
                .ForMember(dest => dest.MesUnitOfMeasureFK, opt => opt.MapFrom(src => src.MesUnitOfMeasureDTOFK))
                .ForMember(dest => dest.SapUnitOfMeasureFK, opt => opt.MapFrom(src => src.SapUnitOfMeasureDTOFK));

            CreateMap<MesParam, MesParamDTO>()
                .ForMember(dest => dest.MesParamSourceTypeDTOFK, opt => opt.MapFrom(src => src.MesParamSourceTypeFK))
                .ForMember(dest => dest.MesDepartmentDTOFK, opt => opt.MapFrom(src => src.MesDepartmentFK))
                .ForMember(dest => dest.SapEquipmentSourceDTOFK, opt => opt.MapFrom(src => src.SapEquipmentSourceFK))
                .ForMember(dest => dest.SapEquipmentDestDTOFK, opt => opt.MapFrom(src => src.SapEquipmentDestFK))
                .ForMember(dest => dest.MesMaterialDTOFK, opt => opt.MapFrom(src => src.MesMaterialFK))
                .ForMember(dest => dest.SapMaterialDTOFK, opt => opt.MapFrom(src => src.SapMaterialFK))
                .ForMember(dest => dest.MesUnitOfMeasureDTOFK, opt => opt.MapFrom(src => src.MesUnitOfMeasureFK))
                .ForMember(dest => dest.SapUnitOfMeasureDTOFK, opt => opt.MapFrom(src => src.SapUnitOfMeasureFK));

            CreateMap<ReportTemplate, ReportTemplateDTO>()
                .ForMember(dest => dest.AddUserDTOFK, opt => opt.MapFrom(src => src.AddUserFK))
                .ForMember(dest => dest.ReportTemplateTypeDTOFK, opt => opt.MapFrom(src => src.ReportTemplateTypeFK))
                .ForMember(dest => dest.DestDataTypeDTOFK, opt => opt.MapFrom(src => src.DestDataTypeFK))
                .ForMember(dest => dest.MesDepartmentDTOFK, opt => opt.MapFrom(src => src.MesDepartmentFK));

            CreateMap<ReportTemplateDTO, ReportTemplate>()
                    .ForMember(dest => dest.AddUserFK, opt => opt.MapFrom(src => src.AddUserDTOFK))
                    .ForMember(dest => dest.ReportTemplateTypeFK, opt => opt.MapFrom(src => src.ReportTemplateTypeDTOFK))
                    .ForMember(dest => dest.DestDataTypeFK, opt => opt.MapFrom(src => src.DestDataTypeDTOFK))
                    .ForMember(dest => dest.MesDepartmentFK, opt => opt.MapFrom(src => src.MesDepartmentDTOFK));

            CreateMap<ReportTemplateTypeTоRole, ReportTemplateTypeTоRoleDTO>()
                    .ForMember(dest => dest.ReportTemplateTypeDTOFK, opt => opt.MapFrom(src => src.ReportTemplateTypeFK))
                    .ForMember(dest => dest.RoleDTOFK, opt => opt.MapFrom(src => src.RoleFK));

            CreateMap<ReportTemplateTypeTоRoleDTO, ReportTemplateTypeTоRole>()
                    .ForMember(dest => dest.ReportTemplateTypeFK, opt => opt.MapFrom(src => src.ReportTemplateTypeDTOFK))
                    .ForMember(dest => dest.RoleFK, opt => opt.MapFrom(src => src.RoleDTOFK));


            CreateMap<UserToRole, UserToRoleDTO>()
                    .ForMember(dest => dest.UserDTOFK, opt => opt.MapFrom(src => src.UserFK))
                    .ForMember(dest => dest.RoleDTOFK, opt => opt.MapFrom(src => src.RoleFK));

            CreateMap<UserToRoleDTO, UserToRole>()
                    .ForMember(dest => dest.UserFK, opt => opt.MapFrom(src => src.UserDTOFK))
                    .ForMember(dest => dest.RoleFK, opt => opt.MapFrom(src => src.RoleDTOFK));

            CreateMap<UserToDepartment, UserToDepartmentDTO>()
                    .ForMember(dest => dest.UserDTOFK, opt => opt.MapFrom(src => src.UserFK))
                    .ForMember(dest => dest.DepartmentDTOFK, opt => opt.MapFrom(src => src.DepartmentFK));

            CreateMap<UserToDepartmentDTO, UserToDepartment>()
                    .ForMember(dest => dest.UserFK, opt => opt.MapFrom(src => src.UserDTOFK))
                    .ForMember(dest => dest.DepartmentFK, opt => opt.MapFrom(src => src.DepartmentDTOFK));

            CreateMap<ReportEntity, ReportEntityDTO>()
                    .ForMember(dest => dest.ReportTemplateDTOFK, opt => opt.MapFrom(src => src.ReportTemplateFK))
                    .ForMember(dest => dest.ReportDepartmentDTOFK, opt => opt.MapFrom(src => src.ReportDepartmentFK))
                    .ForMember(dest => dest.UploadUserDTOFK, opt => opt.MapFrom(src => src.UploadUserFK))
                    .ForMember(dest => dest.DownloadUserDTOFK, opt => opt.MapFrom(src => src.DownloadUserFK));

            CreateMap<ReportEntityDTO, ReportEntity>()
                    .ForMember(dest => dest.ReportTemplateFK, opt => opt.MapFrom(src => src.ReportTemplateDTOFK))
                    .ForMember(dest => dest.ReportDepartmentFK, opt => opt.MapFrom(src => src.ReportDepartmentDTOFK))
                    .ForMember(dest => dest.UploadUserFK, opt => opt.MapFrom(src => src.UploadUserDTOFK))
                    .ForMember(dest => dest.DownloadUserFK, opt => opt.MapFrom(src => src.DownloadUserDTOFK));

            CreateMap<ReportEntityLog, ReportEntityLogDTO>()
                    .ForMember(dest => dest.ReportEntityDTOFK, opt => opt.MapFrom(src => src.ReportEntityFK));

            CreateMap<ReportEntityLogDTO, ReportEntityLog>()
                    .ForMember(dest => dest.ReportEntityFK, opt => opt.MapFrom(src => src.ReportEntityDTOFK));
        }
    }
}
