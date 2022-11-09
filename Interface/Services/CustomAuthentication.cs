using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebProjectAPI.Data;
using WebProjectAPI.Data.Entities;
using WebProjectAPI.Interface;

namespace WebProjectAPI.Services
{
    public class CustomAuthentication : ICustomAuthentication
    {
        private readonly IList<User> users = new List<User>
        { 
            new User {Username = "test1", Password = "password1", Role = "Administrator"},
            new User {Username = "test2", Password = "password2", Role = "Guest"}
        };

        private readonly IDictionary<string, Tuple<string,string>> tokens = 
            new Dictionary<string, Tuple<string, string>>();

        public IDictionary<string, Tuple<string, string>> Tokens => tokens;
        public string Authenticate(string userid, string password)
        {
            if (!users.Any(u => u.Username == userid && u.Password == password))
            {
                return null;
            }

            var token = Guid.NewGuid().ToString();

            tokens.Add(token,new Tuple<string, string>(userid,
                users.First(u => u.Username == userid && u.Password == password).Role));

            return token;
        }
    }
}
