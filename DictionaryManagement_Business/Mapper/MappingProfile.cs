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
            CreateMap<ErrorCriterion, ErrorCriterionDTO>().ReverseMap();
            CreateMap<CorrectionReason, CorrectionReasonDTO>().ReverseMap();

            CreateMap<MesParamSourceType, MesParamSourceTypeDTO>().ReverseMap();
            CreateMap<DataType, DataTypeDTO>().ReverseMap();
            CreateMap<DataSource, DataSourceDTO>().ReverseMap();
            CreateMap<ReportTemplateType, ReportTemplateTypeDTO>().ReverseMap();
            CreateMap<LogEventType, LogEventTypeDTO>().ReverseMap();
            CreateMap<Settings, SettingsDTO>().ReverseMap();

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
        }
    }
}
