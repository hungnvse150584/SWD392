﻿using Booking.Application.Catalog.Parties;
using Booking.Application.Catalog.Products;
using Booking.Data.EF;
using BookingSolution.Utilities.Constants;
using Google;
using Microsoft.EntityFrameworkCore;

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
            services.AddDbContext<BookingDBContext>(options =>
                options.UseSqlServer(
                Configuration.GetConnectionString(SystemConstants.MainConnectionString),
                    options => options.EnableRetryOnFailure()));

            //Khai báo
            services.AddTransient<IPublicProductService, PublicProductService>();
            services.AddTransient<IManagePartyService, ManagePartyService >();

            services.AddControllersWithViews();
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