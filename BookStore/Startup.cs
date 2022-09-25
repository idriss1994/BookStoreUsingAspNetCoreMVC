using BookStore.Data;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookstoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AppConnLocalDb"));
            });
#if DEBUG
            //Uncoment this code to disabled client side validation.
            //services.AddRazorPages().AddViewOptions(options =>
            //{
            //    options.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif
            services.AddControllersWithViews();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookstoreDbContext>();

            services.Configure<NewBookAlertConfig>("NewBookAlert", Configuration.GetSection("NewBookAlert"));
            services.Configure<NewBookAlertConfig>("ThirdPartyBookAlert", Configuration.GetSection("ThirdPartyBookAlert"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllerRoute(
                //    name: "Default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(
                //   name: "about us",
                //   pattern: "about-us",
                //   defaults: new { controller = "home", action = "about" });
                //endpoints.MapControllerRoute(
                //    name: "all books",
                //    pattern: "books",
                //    defaults: new { Controller = "Book", Action = "GetAllBooks" });
            });

        }
    }
}
