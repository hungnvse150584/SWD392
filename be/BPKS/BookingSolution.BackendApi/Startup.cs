using Booking.Application.Catalog.Parties;
using Booking.Application.Catalog.Products;
using Booking.Application.Users;
using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Booking.Application.Catalog.Products;
using Booking.Application.Catalog.Rooms;

namespace BookingSolution.BackendApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        //Call run time
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookingDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString)));

            services.AddIdentity<Account, Role>()
                .AddEntityFrameworkStores<BookingDbContext>()
                .AddDefaultTokenProviders();

            //Khai báo
            services.AddScoped<IManageProductService, ManageProductService>();
            services.AddTransient<IPublicProductService, PublicProductService>();

            services.AddTransient<IManagePartyService, ManagePartyService>();

            services.AddScoped<IManageRoomService, ManageRoomService>();
            services.AddTransient<IPublicRoomService, PublicRoomService>();
            services.AddTransient<IManageRoomService, ManageRoomService>();

            services.AddTransient<IManagePartyService, ManagePartyService >();
            services.AddTransient<UserManager<Account>, UserManager<Account>>();
            services.AddTransient<SignInManager<Account>, SignInManager <Account>>();
            services.AddTransient<RoleManager<Role>, RoleManager<Role>>();
            services.AddTransient<IUserService, UserService>();


            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Product Demo", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Swagger Party Demo", Version = "v1" });

            }
                );
        }

        //http
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Product V1");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Party V1");

            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id}");

            });
        }
    }
}
