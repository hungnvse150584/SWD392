using Booking.Application.Catalog.Parties;
using Booking.Application.Catalog.Products;
using Booking.Application.Users;
using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Constants;
using BookingSolution.ViewModels.System.Users;
using DocumentFormat.OpenXml.Bibliography;
using FluentValidation.AspNetCore;
using Google;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Booking.Application.Catalog.Rooms;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

      

            services.AddIdentity<AspNetUsers, Role>()
                .AddEntityFrameworkStores<BookingDbContext>()
                .AddDefaultTokenProviders();

         

            //Khai báo
            services.AddTransient<IPublicProductService, PublicProductService>();

            services.AddTransient<IManagePartyService, ManagePartyService>();
            services.AddScoped<IManageRoomService, ManageRoomService>();
            services.AddTransient<IPublicRoomService, PublicRoomService>();
            services.AddTransient<IManageProductService, ManageProductService>();
            services.AddTransient<UserManager<AspNetUsers>, UserManager<AspNetUsers>>();
            services.AddTransient<SignInManager<AspNetUsers>, SignInManager <AspNetUsers>>();
            services.AddTransient<RoleManager<Role>, RoleManager<Role>>();

            services.AddTransient<IUserService, UserService>();

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());


            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Product Demo", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Swagger Party Demo", Version = "v1" });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
            });

            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });
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
