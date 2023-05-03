using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniPinterest.Web.Data;
using MiniPinterest.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MiniPinterestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MiniPinterestConnectionString"))
);

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MiniPinterestAuthDbConnectionString"))
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<IPinRepository, PinRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
