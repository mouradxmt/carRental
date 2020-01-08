using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ExamProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ExamProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //Added code
            services.Configure<RequestLocalizationOptions>(
        opts =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                //new CultureInfo("en-GB"),
                new CultureInfo("en-US"),
                //new CultureInfo("en"),
                new CultureInfo("fr-FR"),
                //new CultureInfo("fr"),
            };

           // opts.DefaultRequestCulture = new RequestCulture("en-US");
            // Formatting numbers, dates, etc.
            opts.SupportedCultures = supportedCultures;
            // UI strings that we have localized.
            opts.SupportedUICultures = supportedCultures;
        });


            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<CarsRentalContext>().AddDefaultTokenProviders().AddDefaultUI();
            services.AddLocalization(opts => {
                opts.ResourcesPath = "Resources";
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddViewLocalization(
            LanguageViewLocationExpanderFormat.Suffix,
            opts => { opts.ResourcesPath = "Resources"; })
        .AddDataAnnotationsLocalization();
            services.AddDbContext<CarsRentalContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CarsRentalContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();
            app.UseAuthentication();
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
