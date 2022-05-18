using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        
        public AccountController(DataContext context)
        {
             
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(String userName , String password)
        {
               using (var hmac = new HMACSHA512()) {
               var user = new AppUser() {
                  UserName = userName,
                  PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                  PasswordSalt = hmac.Key
               };

               this._context.Users.Add(user);
               await this._context.SaveChangesAsync();
               return user;
           }
           
        }
         
    }
}