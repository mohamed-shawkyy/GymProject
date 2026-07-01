using Gym.DAL.Interfaces;
using Gym.DAL.Repositories;
using Gym.DAL.AppDbContext;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Gym.BLL.Contacts;
using Gym.BLL.Services;
using Gym.BLL.MappingProfiles;
using Gym.DAL;
using System.Threading.Tasks;
using Gym.Extensions;
namespace Gym
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            //builder.Services.AddScoped<IPlanRepository,PlanRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddDbContext<GymDbContext>(options =>
            {  
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));
            //builder.Services.AddAutoMapper(m=>m.AddProfile(new MappingProfile()));
            var app = builder.Build();

            await app.IntializeDatabaseAsync(); 

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
