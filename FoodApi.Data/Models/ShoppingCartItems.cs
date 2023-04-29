﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodApi.Data.Models
{
    [Table("ShoppingCartItems", Schema = "dbo")]
    public partial class ShoppingCartItems
    {
        [Key]
        public long Id { get; set; } 
        public long Qty { get; set; }
        public long ProductId { get; set; }
        public long CustomerId { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("ShoppingCartItems")]
        public virtual Products Product { get; set; }
    }
}