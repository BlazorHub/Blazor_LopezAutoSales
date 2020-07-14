﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LopezAutoSales.Shared.Models
{
    public class Sale
    {
        public string Id { get; set; }
        public int? LienholderId { get; set; }
        public Lienholder Lienholder { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }

        public DateTime Date { get; set; }
        public string Phone { get; set; }
        public string Buyer { get; set; }
        public string CoBuyer { get; set; }
        public Car Car { get; set; }
        public Car TradeIn { get; set; }
        [Column(TypeName = "money")]
        public decimal SellingPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? BoughtPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal DownPayment { get; set; }
        [Column(TypeName = "money")]
        public decimal MonthlyPayment { get; set; }
        [Column(TypeName = "decimal(5,5)")]
        public decimal TaxRate { get; set; }
        public bool HasTag { get; set; }
        public bool IsOutOfState { get; set; }
        public bool IsPaid { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}