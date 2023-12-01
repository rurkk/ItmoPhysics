
using ItmoPhysics.Controllers;
using ItmoPhysics.Data;
using ItmoPhysics.Servicies;
using ItmoPhysics.Servicies.MatrixInfoUpload;
using ItmoPhysics.Servicies.MatrixInfoUpload.CellsUploads;
using ItmoPhysics.Servicies.MatrixInfoUpload.MatrixUploads;
using ItmoPhysics.Servicies.MatrixInfoUpload.UserFileUploads;
using ItmoPhysics.Servicies.MatrixValidators;
using ItmoPhysics.Servicies.MatrixValidators.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<IMatrixUploadService, MatrixUploadService>();
builder.Services.AddTransient<ICurvesUploadService, CurvesUploadService>();

builder.Services.AddTransient<IMatrixUploadBuilderFactory, MatrixUploadBuilderFactory>();
builder.Services.AddTransient<ICurvesUploadBuilderFactory, CurvesUploadBuilderFactory>();
builder.Services.AddTransient<IUserFileUploadBuilderFactory, UserFileUploadBuilderFactory>();

builder.Services.AddTransient<IMatrixStreamValidator, MatrixStreamValidator>();

builder.Services.AddTransient<IMatrixValidatorHandlerChainFactory, MatrixValidatorHandlerChainFactory>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
