﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodApi.Data.Models
{
    [Table("Users", Schema = "dbo")]
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [StringLength(30)]
        public string Role { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}