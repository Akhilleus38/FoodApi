using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FoodAppApi.Models
{
   
    public class ProductApiModel
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsPopularProduct { get; set; }
        public int CategoryId { get; set; }

        [NotMapped]
        //[JsonIgnore]
        public byte[] ImageArray { get; set; }
    }
}
