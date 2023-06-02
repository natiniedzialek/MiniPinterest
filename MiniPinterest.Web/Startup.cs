using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using MiniPinterest.Web.Authorization;
using MiniPinterest.Web.Data;
using MiniPinterest.Web.Helpers.Abstract;
using MiniPinterest.Web.Helpers;
using MiniPinterest.Web.Repositories;
using Microsoft.AspNetCore.Builder;

public class Startup
{
    private readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddDbContext<MiniPinterestDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MiniPinterestConnectionString"))
        );

        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MiniPinterestAuthDbConnectionString"))
        );

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>();

        services.AddScoped<IPinHelper, PinHelper>();

        services.AddScoped<IBoardRepository, BoardRepository>();
        services.AddScoped<IPinRepository, PinRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IPinLikeRepository, PinLikeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPinCommentRepository, PinCommentRepository>();

        services.AddScoped<IAuthorizationHandler, UserIsPinAuthorAuthorizationHandler>();
        services.AddScoped<IAuthorizationHandler, UserIsBoardAuthorAuthorizationHandler>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("UserIsPinAuthorPolicy", policy => policy.Requirements.Add(new UserIsPinAuthorRequirement()));
            options.AddPolicy("UserIsBoardAuthorPolicy", policy => policy.Requirements.Add(new UserIsBoardAuthorRequirement()));
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = new PathString("/Shared/AccessDenied");
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        });
    }
}