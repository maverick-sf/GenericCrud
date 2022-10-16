using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_assignment.Models
{
    public class IdentityUserRoles:IdentityUserRole
    {
        [Key]
        public int id { get; set; }
    }
}