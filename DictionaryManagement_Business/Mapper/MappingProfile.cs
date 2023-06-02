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

        }
    }
}
