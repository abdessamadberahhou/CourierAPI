using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApi.Services.UserRepository;
using CourierApi.Models.Configuration;
using CourierApi.Services;
using CourierApi.Services.Authenticator;
using CourierApi.Services.PasswordHash;
using CourierApi.Services.TokensGenerators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CourierApi.Services.CourierRepository;
using Microsoft.AspNetCore.Http;
using CourierApi.Services.TokenValidators;
using CourierApi.Services.RefreshTokenRepository;

namespace CourierApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public readonly IConfiguration _configuration ;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();


            AuthConfiguration authConfiguration = new AuthConfiguration();
            _configuration.Bind("Authentication", authConfiguration);
            services.AddSingleton(authConfiguration);

            services.AddSingleton<AccessTokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddSingleton<TokenGenerator>();
            services.AddSingleton<Authenticator>();
            services.AddSingleton<IPasswordHasher, PasswordHashImp>();
            services.AddSingleton<IRefreshTokenRepository, MemoryRefreshTokenRepository>();
            services.AddSingleton<IUserRepository, UserSaver>();
            services.AddSingleton<ICourierRepository, CourierSaver>();



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {

                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.AccessTokenSecret)),
                    ValidIssuer = authConfiguration.Issuer,
                    ValidAudience = authConfiguration.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CourierApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CourierApi v1"));

            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
