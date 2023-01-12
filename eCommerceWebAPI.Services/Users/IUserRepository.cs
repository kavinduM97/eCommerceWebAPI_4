using eCommerceWebAPI.DataAccess;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;
using eCommerceWebAPI.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Services.Users
{
    public interface IUserRepository
    {
        public User GetUser(UserRequest request);
        public List<User> AllUsers();
        public UserErrorHandler UserRegistration(UserRequest request);
        public UserErrorHandler UserLoging(UserRequest request);
    }
}
