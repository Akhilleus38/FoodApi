using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodApi.Data;
using FoodApi.Data.Models;
using FoodAppApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private FoodAppDbContext _dbContext;
        public OrdersController(FoodAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // For Admin
        // GET: api/Orders/PendingOrders
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult PendingOrders()
        {
            //var orders = _dbContext.Orders.Where(order => order.IsOrderCompleted == false);
            //return Ok(orders);
            var orders = _dbContext.Orders.Where(order => order.IsOrderCompleted == true).Select(a => new OrderListApiModel
            {
                Id = a.Id,
                OrderTotal = a.OrderTotal,
                OrderPlaced = a.OrderPlaced,

            }).ToList();
            return Ok(orders);
        }

        // GET: api/Orders/CompletedOrders
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult CompletedOrders()
        {
            //var orders = _dbContext.Orders.Where(order => order.IsOrderCompleted == true);
            //return Ok(orders);

            var orders = _dbContext.Orders.Where(order => order.IsOrderCompleted == true).Select(a => new OrderListApiModel
            {
Id = a.Id,
OrderTotal = a.OrderTotal,
OrderPlaced = a.OrderPlaced,

            }).ToList();
            return Ok(orders);
        }
        // GET: api/Orders/OrderDetails/5
        [HttpGet("[action]/{orderId}")]
        public IActionResult OrderDetails(int orderId)
        {

            var orders = _dbContext.Orders.Where(order => order.Id == orderId).Select(a => new OrderListApiModel
            {
                Address = a.Address,
                FullName = a.FullName,
                IsOrderCompleted = a.IsOrderCompleted,
                OrderPlaced = a.OrderPlaced,
                OrderTotal = a.OrderTotal,
                Phone = a.Phone,
                UserId = a.UserId,
                Id = a.Id,
                OrderDetails = (from t in a.OrderDetails
                                select new OrderListDetailModel
                                {
                                    Price = t.Price,
                                    Id = t.Id,
                                    OrderId = t.OrderId,
                                    ProductId = t.ProductId,
                                    Qty = t.Qty,
                                    TotalAmount = t.TotalAmount,

                                    Product = new OrderListProductModel
                                    {
                                        Name = t.Product.Name,
                                       // CategoryId = t.Product.CategoryId,
                                       // Detail = t.Product.Detail,
                                        ImageUrl = t.Product.ImageUrl,
                                       // IsPopularProduct = t.Product.IsPopularProduct,
                                        //Price = t.Product.Price

                                    }
                                }).ToList()
            });

            return Ok(orders);
        }


        // GET: api/Orders/OrdersCount
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult OrdersCount()
        {
            var orders = (from order in _dbContext.Orders
                          where order.IsOrderCompleted == false
                          select order.IsOrderCompleted).Count();
            return Ok(new { PendingOrders = orders });
        }


        // GET: api/Orders/OrdersByUser/5
        [HttpGet("[action]/{userId}")]
        public IActionResult OrdersByUser(int userId)
        {
            var orders = _dbContext.Orders.Where(order => order.UserId == userId).OrderByDescending(o => o.OrderPlaced);
            return Ok(orders);
        }

        // POST: api/Orders
        [HttpPost]
        public IActionResult Post([FromBody] OrderApiModel model)
        {

            var shoppingCartItems = _dbContext.ShoppingCartItems.Where(cart => cart.CustomerId == model.UserId);
            if (!shoppingCartItems.Any())
            {
                return NotFound("User Shopping Cart Items Not Found...");
            }
            var order = new Orders
            {
                OrderPlaced = DateTime.Now,
                IsOrderCompleted = true,
                FullName = model.FullName,
                UserId = model.UserId,
                Address = model.Address,
                Phone = model.Phone,
                OrderTotal = model.OrderTotal,

            };

            order.OrderDetails = (from od in shoppingCartItems
                                  select new OrderDetails
                                  {
                                      Price = od.Product.Price,
                                      ProductId = od.ProductId,
                                      Qty = od.Qty,
                                      TotalAmount = od.Product.Price * od.Qty,
                                  }).ToList();


            _dbContext.ShoppingCartItems.RemoveRange(shoppingCartItems);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return Ok(new { OrderId = order.Id });
        }


        // PUT: api/Orders/MarkOrderComplete/5
        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{orderId}")]
        public IActionResult MarkOrderComplete(int orderId)
        {
            var entity = _dbContext.Orders.FirstOrDefault(x => x.Id == orderId);
            if (entity == null)
            {
                return NotFound("No order found against this id...");
            }
            else
            {
                entity.IsOrderCompleted = true;
                _dbContext.SaveChanges();
                return Ok("Order completed");
            }
        }
    }
}
