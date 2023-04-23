using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodAppApi.Models
{
    
    public class OrderApiModel
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool IsOrderCompleted { get; set; }
        public int UserId { get; set; }
    }
    //public class OrderDetailApiModel
    //{
    //    public decimal Price { get; set; }
    //    public int Qty { get; set; }
    //    public decimal TotalAmount { get; set; }
    //    public int ProductId { get; set; }
    //}
}
