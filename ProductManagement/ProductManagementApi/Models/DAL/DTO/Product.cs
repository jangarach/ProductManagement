﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementApi.Models.DAL.DTO
{
    [Table("Product")]
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}