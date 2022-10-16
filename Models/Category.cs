using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Final_assignment.Base;

namespace Final_assignment.Models
{
    public class Category:BaseEntity
    {
        public Category()
        {

        }
        public List<Product> Products { get; set; }
    }
}