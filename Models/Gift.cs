using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MGC.Models
{
    public class Gift
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string StoreLink { get; set; }
        public Recipient Recipient { get; set; }
        public Holiday Holiday { get; set; }
        public string Notes { get; set; }
        public bool Purchased { get; set; }
        public bool Wrapped { get; set; }
    }
}