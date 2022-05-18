using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Globalization;

//TODO: Make these using statements match your project
using  Kim_Angela_Eonjeong_HW4.Models;
using  Kim_Angela_Eonjeong_HW4.DAL;

//TODO: Make this namespace match your project - be sure to remove the []
namespace Kim_Angela_Eonjeong_HW4
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //This adds the MVC engine and Razor code
            services.AddControllersWithViews();

            //TODO: (For HW4 and beyond) Add a connection string here once you have created it on Azure
            String connectionString = "Server=tcp:sp22kimangelaeonjeonghw4.database.windows.net,1433;Initial Catalog=sp22KimAngelaEonjeonghw4;Persist Security Info=False;User ID=MISAdmin;Password=Password1234;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            //TODO: Uncomment this line once you have your connection string
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            //TODO: Uncomment these lines once you have added Identity to your project
            ////NOTE: This is where you would change your password requirements
            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app)
        {
            //These lines allow you to see more detailed error messages
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();

            //This line allows you to use static pages like style sheets and images
            app.UseStaticFiles();

            //This marks the position in the middleware pipeline where a routing decision
            //is made for a URL.
            app.UseRouting();

            //This allows the data annotations for currency to work on a mac
            app.Use(async (context, next) =>
            {
                CultureInfo.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;

                await next.Invoke();
            });

            //TODO: Once you have added Identity into your project, you will need to uncomment these lines
            app.UseAuthentication();
            app.UseAuthorization();

            //This method maps the controllers and their actions to a patter for
            //requests that's known as the default route. This route identifies
            //the Home controller as the default controller and the Index() action
            //method as the default action. The default route also identifies a 
            //third segment of the URL that's a parameter named id.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}