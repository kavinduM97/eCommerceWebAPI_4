using Azure.Core;
using eCommerceWebAPI.DataAccess;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;
using eCommerceWebAPI.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Services.Users
{
    public class UserServices : IUserRepository
    {
        private readonly DbbContext _dbcontext=new DbbContext();
        private UserErrorHandler response;
        private readonly IConfiguration _appSettingsconfiguration;

        public UserServices(IConfiguration appSettingsconfiguration)
        {
            _appSettingsconfiguration = appSettingsconfiguration;
        
        }

        //Get All Users
        public List<User> AllUsers()
        {
            return _dbcontext.Users.ToList();
        }

        public User GetUser(UserRequest request)
        {
            var user = _dbcontext.Users.Where(u => u.Email == request.Email).FirstOrDefault();
            
            return user;
        }
           
        

        //User Reg
        public UserErrorHandler UserRegistration(UserRequest request)
        {
          

            if (_dbcontext.Users.Any(u => u.Email == request.Email))
            {
                response = SetResponse(false, "User already exists", "Customer", null);
                return response;
            }

            try
            {
                 
                var user = new User
                {
                    Email = request.Email,
                    Password = request.Password,
                    IsAdmin = false,
                    Token = "",
                };

                _dbcontext.Users.Add(user);
                _dbcontext.SaveChangesAsync();

                response = SetResponse(true, "User added successfully", "Customer", user);


            }
            catch (Exception ex)
            {
                response = SetResponse(false, "Something went wrong", "Customer", null);

            }

            return response;

        }

        //User Loging

        public UserErrorHandler UserLoging(UserRequest request)
        {
            var user = _dbcontext.Users.Where(u => u.Email == request.Email && u.Password == request.Password).FirstOrDefault(); 
            var admin= _dbcontext.Users.Where(u => u.Email == request.Email && u.Password == request.Password && u.IsAdmin == true).FirstOrDefault(); 
            var customer= _dbcontext.Users.Where(u => u.Email == request.Email && u.Password == request.Password && u.IsAdmin == false).FirstOrDefault();
            if (user == null)
            {
                response = SetResponse(false, "Loging failed", "User name or password is incorrect", user);

                return response;
            }
            else
            {
                if (admin == null)
                {
                    string userjwtToken = CreateJwtTokenUser(customer);

                    var mycustomer = GetUser(request);

                    mycustomer.Token = userjwtToken;
                    _dbcontext.Users.Update(mycustomer);

                    _dbcontext.SaveChangesAsync();
                    response = SetResponse(true, "Customer logged successfully", "Customer", customer);
                    return response;
                }
                string adminjwtToken = CreateJwtTokenAdmin(admin);
                var myadmin = GetUser(request);

                myadmin.Token = adminjwtToken;

                _dbcontext.Users.Update(myadmin);

                _dbcontext.SaveChangesAsync();
                response = SetResponse(true, "Admin logged successfully","Admin", admin);
                return response;
            }
        }

        private string CreateJwtTokenAdmin(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin"),
              

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _appSettingsconfiguration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        private string CreateJwtTokenUser(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "User"),


            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _appSettingsconfiguration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        //Create a Token for user

        private static string SetToken(string email, string password)
        {
            var mySecretString = email + password;

            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(mySecretString)));
        }

        //Set Response

        private UserErrorHandler SetResponse(bool state, string message, string user, User detail)
        {
            UserErrorHandler response = new UserErrorHandler
            {
                State = state,
                Message = message,
                User = user,
                Detail = detail,
            };

            return response;
        }
    }
}
