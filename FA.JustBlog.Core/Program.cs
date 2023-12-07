using FA.JustBlog.Core.Data;
using FA.JustBlog.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//Add connection data service
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("JustBlogConnection")));
builder.Services.AddDbContext<ApplicationDbContext1>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("JustBlogConnection")));
//SETTING JSON FILE
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    areaName :"Admin",
    name: "Admin",
    pattern: "{area=Admin}/{controller=Posts}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    areaName: "Admin",
    name: "sortBy",
    pattern: "{area=Admin}/{controller=Posts}/{action=Index}/{sortBy}");
app.MapAreaControllerRoute(
    areaName: "Admin",
    name: "sortBy1",
    pattern: "{area=Admin}/{controller=Posts}/{sortBy?}",
    defaults: new {controller = "Posts",action = "GetPosts"}
    );
app.MapControllerRoute(
    name: "post",
    pattern: "{controler=Posts}/{year}/{month}/{title}",
    defaults: new { controller ="Posts", action="Details"},
    constraints: new { year = @"\d{4}", month = @"\d{2}" }
    );
app.Run();
