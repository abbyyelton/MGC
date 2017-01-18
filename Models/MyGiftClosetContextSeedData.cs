using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGC.Models
{
    public class MyGiftClosetContextSeedData
    {
        private MyGiftClosetContext _context;
        private UserManager<GiftUser> _userManager;

        public MyGiftClosetContextSeedData(MyGiftClosetContext context, UserManager<GiftUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            var user = new GiftUser();
            if (await _userManager.FindByEmailAsync("ayelton@composablesystems.com") == null)
            {
                user.UserName = "ayelton";
                user.Email = "ayelton@composablesystems.com";
                user.Birthday = new DateTime(1975, 8, 6);

                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }
            user = _context.GiftUsers
                    .Where(gu => gu.UserName == "ayelton")
                    .FirstOrDefault();

            if (!_context.Gifts.Any())
            {
                _context.Holidays.Add(new Holiday() { Name = "Easter", Date = new DateTime(2017, 4, 16) });
                _context.Holidays.Add(new Holiday() { Name = "Hanukkah", Date = new DateTime(2017, 12, 12) });

                _context.Gifts.Add(new Gift()
                {
                    GiftUser = user,
                    Name = "Red Shirt",
                    Holiday = new Holiday() { Name = "Christmas", Date = new DateTime(2017, 12, 25) },
                    Recipient = new Recipient() { Name = "Dad", GiftUser = user, Birthday = new DateTime(1945, 3, 18), Email = "dad@yahoo.com" },
                    Wrapped = false,
                    Purchased = true,
                    Price = 34.50M
                });

                _context.Gifts.Add(new Gift()
                {
                    GiftUser = user,
                    Name = "Necklace",
                    Holiday = new Holiday() { Name = "Valentine's Day", Date = new DateTime(2017, 2, 14) },
                    Recipient = new Recipient() { Name = "Mom", GiftUser = user, Birthday = new DateTime(1948, 9, 9), Email = "mom@yahoo.com" },
                    Wrapped = true,
                    Purchased = true,
                    Price = 20.00M
                });

                await _context.SaveChangesAsync();
            }

        }
    }
}
