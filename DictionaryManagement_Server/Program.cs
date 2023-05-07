using DictionaryManagement_Business.Repository;
using DictionaryManagement_Business.Repository.IRepository;
using DictionaryManagement_DataAccess.Data.IntDB;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Runtime.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<IntDBApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("IntDBConnection")));
builder.Services.AddScoped<ISapEquipmentRepository, SapEquipmentRepository>();
builder.Services.AddScoped<ISapMaterialRepository, SapMaterialRepository>();
builder.Services.AddScoped<IMesMaterialRepository, MesMaterialRepository>();
builder.Services.AddScoped<IMesUnitOfMeasureRepository, MesUnitOfMeasureRepository>();
builder.Services.AddScoped<ISapUnitOfMeasureRepository, SapUnitOfMeasureRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();


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
