using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using KoronaZakupy.Services.Interfaces;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Repositories;
using KoronaZakupy.UnitOfWork;
using AutoMapper;
using KoronaZakupy.Helpers;
using System.Text.Json.Serialization;

namespace KoronaZakupy {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers();

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddJsonOptions(option =>
                {
                    option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddDbContext<Entities.UserDb.UsersDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UsersDatabase")));
            services.AddDbContext<OrdersDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OrdersDatabase")));

            services.AddIdentity<Entities.UserDb.User, IdentityRole>().AddEntityFrameworkStores<Entities.UserDb.UsersDbContext>();
           

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg => {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IUserRegister, UserRegister>();
            services.AddScoped<IUserLogin, UserLogin>();
            services.AddScoped<IUserGetter, UsersGetter>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ICreateOrder, CreateOrder>();
            services.AddScoped<IOrderGetter, OrderGetter>();
            services.AddScoped<IUpdateOrder, UpdateOrder>();
            services.AddScoped<ICompleteUserInfo, CompleteUserInfo>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
