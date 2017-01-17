using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGC.Models
{
    public class GiftUser : IdentityUser
    {
        public DateTime Birthday { get; set; }
    }
}
