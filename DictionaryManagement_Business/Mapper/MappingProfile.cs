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
        }
    }
}
