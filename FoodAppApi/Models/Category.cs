using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodAppApi.Models
{
    public class CategoryApiModel
    {   
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public byte[] ImageArray { get; set; }
    }

    public class CategoryAddApiModel: CategoryApiModel
    {
       
    }
    public class CategoryEditApiModel: CategoryApiModel
    {
        public int Id { get; set; }
       
    }
}
