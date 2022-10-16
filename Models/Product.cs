using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using Final_assignment.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Final_assignment.Models
{
    public class Product:BaseEntity
    {
        public Product()
        {

        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime Expiry { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category{get; set;}

    }

}