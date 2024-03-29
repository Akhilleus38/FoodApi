﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodApi.Data.Models
{
    [Table("Orders", Schema = "dbo")]
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(300)]
        public string FullName { get; set; }
        public string Address { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderTotal { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OrderPlaced { get; set; }
        public bool IsOrderCompleted { get; set; }
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Orders")]
        public virtual Users User { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}