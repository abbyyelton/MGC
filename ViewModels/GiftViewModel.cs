using MGC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGC.ViewModels
{
    public class GiftViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [StringLength(200)]
        public string StoreLink { get; set; }

        public string RecipientName { get; set; }
        public string HolidayName { get; set; }
        public string Notes { get; set; }
        public bool Purchased { get; set; }
        public bool Wrapped { get; set; }
    }
}
