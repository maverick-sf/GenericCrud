using Final_assignment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_assignment.Repository
{
    public interface IAccountRepository
    {


        Task<IdentityResult> CreasteAsync(Register UserSign);
    }
}
