using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Text;

namespace API_MediBridge.Filters
{

    public class JwtAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Extract the JWT token from the "Authorization" header
            string jwtToken = ExtractJwtTokenFromHeader(actionContext.Request.Headers.Authorization);

            if (string.IsNullOrEmpty(jwtToken) || !IsTokenValid(jwtToken))
            {
                // Return an Unauthorized response if the JWT token is not valid
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid JWT token.");
            }

            base.OnActionExecuting(actionContext);
        }

        private string ExtractJwtTokenFromHeader(System.Net.Http.Headers.AuthenticationHeaderValue authorizationHeader)
        {
            if (authorizationHeader == null)
            {
                return null;
            }

            string authorizationHeaderString = authorizationHeader.ToString();
            if (!string.IsNullOrEmpty(authorizationHeaderString) && authorizationHeaderString.StartsWith("Bearer "))
            {
                // Remove "Bearer " to get only the token
                return authorizationHeaderString.Substring("Bearer ".Length);
            }

            return null;
        }

        private bool IsTokenValid(string jwtToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string secretKeyString = System.Configuration.ConfigurationManager.AppSettings["JwtSecretKey"];

            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKeyString);
            var securityKey = new SymmetricSecurityKey(secretKeyBytes);

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidIssuer = System.Configuration.ConfigurationManager.AppSettings["JwtIssuer"],
                ValidAudience = System.Configuration.ConfigurationManager.AppSettings["JwtAudience"],
                IssuerSigningKey = securityKey
            };

            try
            {
                SecurityToken validatedToken;

                ClaimsPrincipal principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out validatedToken);

                // Check if the token has expired
                if (validatedToken.ValidTo < DateTime.UtcNow)
                {
                    return false;
                }

                // Other custom validations, if needed

                return true;
            }
            catch (SecurityTokenValidationException)
            {
                // The token is not valid
                return false;
            }
        }
    }

}