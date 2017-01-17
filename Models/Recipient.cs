using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MGC.Models
{
    public class Recipient
    {
        public Recipient()
        {
            Gifts = new List<Gift>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        ICollection<Gift> Gifts { get; set; }
    }
}