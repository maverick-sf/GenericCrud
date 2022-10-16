using Final_assignment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Final_assignment.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        public AccountRepository()
        {
            FoodStoreContext _context = new FoodStoreContext();
            UserStore<ApplicationUser>userStore=new UserStore<ApplicationUser>(_context);
            _userManager = new UserManager<ApplicationUser>(userStore);

        }

        public Task<IdentityResult> CreasteAsync(Register UserSign)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(Register userModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = userModel.Email,
                //Password = userModel.Password,
                //confirmPassword = userModel.ConfirmPassword
            };

            var result = await _userManager.CreateAsync(user,userModel.Password);
            return result;
           // throw new NotImplementedException();
            
            
        }



    }
}










