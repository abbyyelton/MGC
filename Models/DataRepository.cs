using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace MGC.Models
{
    public class DataRepository : IDataRepository
    {
        private MyGiftClosetContext _context;

        public DataRepository(MyGiftClosetContext context)
        {
            _context = context;
        }

        public IEnumerable<Gift> GetAllGifts(string userName)
        {
            return _context.Gifts
                .Where(g => g.UserName == userName)
                .Include(h => h.Holiday)
                .Include(r => r.Recipient)
                .ToList();
        }

        public Gift GetGift(int Id)
        {
            return _context.Gifts
                .Where(g => g.Id == Id)
                .Include(g => g.Holiday)
                .Include(g => g.Recipient)
                .FirstOrDefault();
        }

        public void AddGift(Gift gift)
        {
            _context.Gifts.Add(gift);
        }

        public void DeleteGift(int Id)
        {
            var gift = GetGift(Id);
            if ( gift != null)
            {
                _context.Gifts.Remove(gift);
            }
        }

        public void UpdateGift(Gift gift)
        {
            var dbGift = GetGift(gift.Id);
            if (dbGift != null)
            {
                dbGift = gift;
            }
        }

        public void UpdateAllGifts(IEnumerable<Gift> gifts)
        {
            
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}