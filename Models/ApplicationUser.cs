using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_assignment.Models
{
    public class ApplicationUser:IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }


    }
}