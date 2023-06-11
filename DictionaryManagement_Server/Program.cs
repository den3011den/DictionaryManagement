using DictionaryManagement_Business.Repository;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_Common;
using DictionaryManagement_DataAccess.Data.IntDB;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Radzen;
using System.Runtime.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.WebHost.UseUrls("http://localhost:7776", "https://localhost:7777");
builder.Services.AddHttpsRedirection(options => options.HttpsPort = 7777);


SD.AppFactoryMode = SD.FactoryMode.KOS;
if (builder.Configuration.GetValue<string>("FactoryMode") == "NKNH")
    SD.AppFactoryMode = SD.FactoryMode.NKNH;

    builder.Services.AddDbContext<IntDBApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("IntDBConnection")));
builder.Services.AddScoped<ISapEquipmentRepository, SapEquipmentRepository>();
builder.Services.AddScoped<ISapMaterialRepository, SapMaterialRepository>();
builder.Services.AddScoped<IMesMaterialRepository, MesMaterialRepository>();
builder.Services.AddScoped<IMesUnitOfMeasureRepository, MesUnitOfMeasureRepository>();
builder.Services.AddScoped<ISapUnitOfMeasureRepository, SapUnitOfMeasureRepository>();
builder.Services.AddScoped<IErrorCriterionRepository, ErrorCriterionRepository>();
builder.Services.AddScoped<ICorrectionReasonRepository, CorrectionReasonRepository>();

builder.Services.AddScoped<IMesParamSourceTypeRepository, MesParamSourceTypeRepository>();
builder.Services.AddScoped<IDataTypeRepository, DataTypeRepository>();
builder.Services.AddScoped<IDataSourceRepository, DataSourceRepository>();
builder.Services.AddScoped<IReportTemplateTypeRepository, ReportTemplateTypeRepository>();
builder.Services.AddScoped<ILogEventTypeRepository, LogEventTypeRepository>();
builder.Services.AddScoped<ISettingsRepository, SettingsRepository>();
builder.Services.AddScoped<IUnitOfMeasureSapToMesMappingRepository, UnitOfMeasureSapToMesMappingRepository>();
builder.Services.AddScoped<ISapToMesMaterialMappingRepository, SapToMesMaterialMappingRepository>();
builder.Services.AddScoped<IMesDepartmentRepository, MesDepartmentRepository>();
builder.Services.AddScoped<IMesParamRepository, MesParamRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserToRoleRepository, UserToRoleRepository>();
builder.Services.AddScoped<IUserToDepartmentRepository, UserToDepartmentRepository>();

builder.Services.AddScoped<IReportTemplateRepository, ReportTemplateRepository>();
builder.Services.AddScoped<IReportTemplateTîDepartmentRepository, ReportTemplateTîDepartmentRepository>();
builder.Services.AddScoped<IReportTemplateTypeTîRoleRepository, ReportTemplateTypeTîRoleRepository>();
builder.Services.AddScoped<IReportEntityRepository, ReportEntityRepository>();
builder.Services.AddScoped<IReportEntityLogRepository, ReportEntityLogRepository>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();




System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
SD.AppVersion = fvi.FileVersion;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
