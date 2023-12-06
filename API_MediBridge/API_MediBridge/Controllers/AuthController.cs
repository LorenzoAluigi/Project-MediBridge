using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Configuration;
using Microsoft.IdentityModel.Tokens;
using API_MediBridge.Models.Class;
using API_MediBridge.Models;

namespace API_MediBridge.Controllers
{
    public class AuthController : ApiController
    {
        ModelDbContext dbContext = new ModelDbContext();

        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult Login([FromBody] Login model)
        {
            // Check if the user is valid based on the provided username and password
            if (IsUserValid(model.Email, model.Password))
            {
                // Retrieve the user based on the provided email
                Users user = dbContext.Users.FirstOrDefault(e=>e.Email == model.Email);
                
                // Extract user roles from the database
                var userRoles = user.UserRoles.Select(e => e.Roles.RoleName).ToArray();

                // Create a JWT security token for the authenticated user
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WebConfigurationManager.AppSettings["JwtSecretKey"]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    // Set the subject of the token with the user's unique identifier
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UID", user.UserId.ToString())

                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512),
                    Audience = WebConfigurationManager.AppSettings["JwtAudience"],
                    Issuer = WebConfigurationManager.AppSettings["JwtIssuer"]
                };

                // Add user roles as claims to the token
                tokenDescriptor.Subject.AddClaims(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                // Create the JWT token
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Return the JWT token
                return Ok(new { token = tokenString });
            }
            else
            {
                // If the user is not valid, return an Unauthorized response
                return Unauthorized();
            }
        }

        private bool IsUserValid(string email, string password)
        {
            //"I'm checking if there is a registered user with this email in the database.
            Users user = dbContext.Users.FirstOrDefault(e => e.Email == email);
            
            //If the user is not found, I'll return 'false'.
            if (user == null)
            {
                return false;
            }

            // Verify if the entered password matches the user's hashed password
            bool verified = BCrypt.Net.BCrypt.Verify(password, user.Password);

            // Return the verification result
            return (verified);
        }


        [HttpPost]
        [Route("api/register")]
        public IHttpActionResult Register([FromBody] Users user)
        {
            // Check if the email already exists in the database
            bool emailExists = dbContext.Users.Any(e => e.Email == user.Email);

            if (emailExists)
            {
                // Return a BadRequest response if the email already exists, indicating that registration failed.
                return BadRequest("Email address already exists. Registration failed.");
            }

            // Hash the user's password before storing it in the database
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Update the user's password with the hashed version
            user.Password = hashPassword;

            // Add the user to the database and save changes
            try 
            { 
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            }
            catch(Exception ex) 
            {
                return BadRequest("Registration failed: " + ex.Message);
            }

            // Return an OK response to indicate successful user registration
            return Ok("User registered successfully");

        }
    }
}
