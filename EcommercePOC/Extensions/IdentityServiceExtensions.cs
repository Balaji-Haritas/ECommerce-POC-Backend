using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EcommercePOC.Extensions
{
    public  static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                var token = config["TokenKey"] ?? throw new Exception("Token Key not found");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            //services.AddAuthorization(options =>
            //{
            //    // Add a policy to require specific roles (optional but useful for role-based authorization)
            //    options.AddPolicy("AdminUserPolicy", policy => policy.RequireRole("Admin", "User"));
            //});

            return services; 
        }
    }
}
