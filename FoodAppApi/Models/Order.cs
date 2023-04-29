using FoodApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodAppApi.Models
{

    public class OrderApiModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool IsOrderCompleted { get; set; }
        public int UserId { get; set; }
    }
    public class OrderListApiModel
    {
        public OrderListApiModel()
        {
            OrderDetails = new List<OrderListDetailModel>();
        }
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool IsOrderCompleted { get; set; }
        public long UserId { get; set; }
        public List<OrderListDetailModel> OrderDetails { get; set; }
    }
    public class OrderListDetailModel
    {
        public OrderListDetailModel()
        {
            Product = new OrderListProductModel();
        }
        public long Id { get; set; }
        public decimal Price { get; set; }
        public long Qty { get; set; }    
        public decimal TotalAmount { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public OrderListProductModel Product { get; set; }
    }
    public class OrderListProductModel
    {
        //public int id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsPopularProduct { get; set; }
        public long CategoryId { get; set; }
        public object ImageArray { get; set; }

    }
    //public class OrderDetailApiModel
    //{
    //    public decimal Price { get; set; }
    //    public int Qty { get; set; }
    //    public decimal TotalAmount { get; set; }
    //    public int ProductId { get; set; }
    //}
}
