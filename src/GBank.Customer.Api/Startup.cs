using System;
using System.Text;
using FluentValidation.AspNetCore;
using IdentityModel.Client;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GBank.Customer.Api
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
            IdentityModelEventSource.ShowPII = true;
            services.AddDbContext<CustomerDbContext>(options => options.UseInMemoryDatabase(databaseName: "CustomerDb"));
            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            #region Http Services
            services.AddAccessTokenManagement(options =>
            {
                options.Client.Clients.Add("AccountClient", new ClientCredentialsTokenRequest
                {
                    Address = $"{Configuration.GetSection("IdentityAddress").Value}/connect/token",
                    ClientId = Configuration.GetSection("AccountApi:ClientId").Value,
                    ClientSecret = Configuration.GetSection("AccountApi:ClientSecret").Value,
                    Scope = Configuration.GetSection("AccountApi:Scope").Value,
                    GrantType = "client_credentials"
                });
            });

            services.AddClientAccessTokenHttpClient("AccountClient", configureClient: client =>
           {
               client.BaseAddress = new Uri(Configuration.GetSection("AccountApi:Address").Value);
           });

            services.AddTransient<AccountClient>();
            #endregion

            #region DI Registration
            services.AddSingleton<IMapper, Mapper>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddSingleton<AccountMessageSender>();
            #endregion

            #region Auth
            var key = Encoding.ASCII.GetBytes("GBankCustomerSecret");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Authority = Configuration.GetSection("IdentityAddress").Value;
                x.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            Console.WriteLine("Config burada  " + Configuration.GetSection("IdentityAddress").Value);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthConstants.RoleAdmin, policy =>
                    policy.RequireClaim("scope", AuthConstants.ReadScope, AuthConstants.WriteScope));

                options.AddPolicy(AuthConstants.RoleUser, policy =>
                    policy.RequireClaim("scope", AuthConstants.ReadScope));
            });


            #endregion

            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<AddCustomerDto>();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GBank.Customer.Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GBank.Customer.Api v1"));
            }
            app.InitializeSampleData();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
