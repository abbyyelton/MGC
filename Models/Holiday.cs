using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MGC.Models
{
    public class Holiday
    {
        public Holiday()
        {
            Gifts = new List<Gift>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }

        public ICollection<Gift> Gifts { get; set; }
    }
}