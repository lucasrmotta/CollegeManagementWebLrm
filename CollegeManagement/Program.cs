using CollegeManagement.Data;
using CollegeManagement.HubConfig;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ----- Add services to the container ----- //
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// ----- DbContext ----- //
string startupPath = Environment.CurrentDirectory;
var ConectionStringLocal = builder.Configuration.GetConnectionString("LocalConnection");

//Replace Content Root Path To User Content
if (ConectionStringLocal.Contains("%CONTENTROOTPATH%"))
{
    ConectionStringLocal = ConectionStringLocal.Replace("%CONTENTROOTPATH%", startupPath);
}

builder.Services.AddDbContext<COLLEGE_MANAGEMENT_DBContext>(options =>
                    options.UseSqlServer(ConectionStringLocal));

builder.Services.AddDbContext<global::CollegeManagement.Data.COLLEGE_MANAGEMENT_DBContext>((global::Microsoft.EntityFrameworkCore.DbContextOptionsBuilder options) =>
    options.UseSqlServer(ConectionStringLocal));

// ---- SignalR ---- //
builder.Services.AddSignalR();

// ----- Builder ----- //
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapHub<DashboardHub>("/RealTimeInfo");
});


app.Run();
