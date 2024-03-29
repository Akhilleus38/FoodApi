﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FoodApi.Data.Models;
using FoodApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartItemsController : ControllerBase
    {
        private FoodAppDbContext _dbContext;
        public ShoppingCartItemsController(FoodAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/ShoppingCartItems
        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            var user = _dbContext.ShoppingCartItems.Where(s => s.CustomerId == userId);
            if (user == null)
            {
                return NotFound();
            }

            var shoppingCartItems = from s in _dbContext.ShoppingCartItems.Where(s => s.CustomerId == userId)
                                    join p in _dbContext.Products on s.ProductId equals p.Id

                                    select new
                                    {
                                        Id = s.Id,
                                        Price = s.Product.Price,
                                        TotalAmount = s.Product.Price*s.Qty,
                                        Qty = s.Qty,
                                        ProductName = p.Name,

                                    };

            return Ok(shoppingCartItems);
        }

        // GET: api/ShoppingCartItems/SubTotal/5

        [HttpGet("[action]/{userId}")]
        public IActionResult SubTotal(int userId)
        {
            var subTotal = (from cart in _dbContext.ShoppingCartItems
                            where cart.CustomerId == userId
                            select cart.Product.Price * cart.Qty).Sum();
            return Ok(new { SubTotal = subTotal });
        }


        // GET: api/ShoppingCartItems/TotalItems/5
        [HttpGet("[action]/{userId}")]
        public IActionResult TotalItems(int userId)
        {
            var cartItems = (from cart in _dbContext.ShoppingCartItems
                             where cart.CustomerId == userId
                             select cart.Qty).Sum();
            return Ok(new { TotalItems = cartItems });
        }

        // POST: api/ShoppingCartItems
        [HttpPost]
        public IActionResult Post([FromBody] ShoppingCartItems shoppingCartItem)
        {
            var shoppingCart = _dbContext.ShoppingCartItems.FirstOrDefault(s => s.ProductId == shoppingCartItem.ProductId && s.CustomerId == shoppingCartItem.CustomerId);
            if (shoppingCart != null)
            {
                shoppingCart.Qty += shoppingCartItem.Qty;
                //shoppingCart.TotalAmount = shoppingCart.Price * shoppingCart.Qty;
            }
            else
            {
                var sCart = new ShoppingCartItems()
                {
                    CustomerId = shoppingCartItem.CustomerId,
                    ProductId = shoppingCartItem.ProductId, 
                    Qty = shoppingCartItem.Qty, 
                };
                _dbContext.ShoppingCartItems.Add(sCart);
            }
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            var shoppingCart = _dbContext.ShoppingCartItems.Where(s => s.CustomerId == userId);
            _dbContext.ShoppingCartItems.RemoveRange(shoppingCart);
            _dbContext.SaveChanges();
            return Ok();

        }
    }
}
