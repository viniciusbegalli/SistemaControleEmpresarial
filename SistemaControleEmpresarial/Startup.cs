using System;
using SistemaControleEmpresarial.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaControleEmpresarial.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaControleEmpresarial.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace SistemaControleEmpresarial
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
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                         .AddEntityFrameworkStores<ApplicationDbContext>()  
                         .AddDefaultTokenProviders()
                         .AddDefaultUI();
            /*
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
                */
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            /*
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                //googleOptions.UserInformationEndpoint = "https://openidconnect.googleapis.com/v1/userinfo";
                //googleOptions.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v1/certs";
                googleOptions.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v3/tokeninfo";
            });
            */
            services.AddAuthentication().AddGoogle(o =>
             {
                 o.ClientId = Configuration["Authentication:Google:ClientId"];
                 o.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                 o.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                 o.ClaimActions.Clear();
                 o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                 o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                 o.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                 o.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                 o.ClaimActions.MapJsonKey("urn:google:profile", "link");
                 o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
             });

            /*
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");
                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });
            */
            services.Configure<IISOptions>(o =>
            {
                o.ForwardClientCertificate = false;
            });

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, AuthMessageSender>();

            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
