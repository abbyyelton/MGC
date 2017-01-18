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
            var giftUser = _getUser(userName);

            return _context.Gifts
                .Where(g => g.GiftUser == giftUser)
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

        public void DeleteGift(Gift gift)
        {
            if (gift != null)
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

        public Holiday GetHolidayByName(string holidayName)
        {
            return _context.Holidays
                .Where(h => h.Name == holidayName)
                .FirstOrDefault();
        }

        public Recipient GetRecipientByName(string recipientName)
        {
            return _context.Recipients
                .Where(r => r.Name == recipientName)
                .FirstOrDefault();
        }

        public GiftUser GetGiftUserByName(string userName)
        {
            return _context.GiftUsers
                .Where(gu => gu.UserName == userName)
                .FirstOrDefault();
        }

        public IEnumerable<Holiday> GetAllHolidays()
        {
            return _context.Holidays.ToList();
        }

        public void AddHoliday(Holiday newHoliday)
        {
            _context.Holidays.Add(newHoliday);
        }

        public IEnumerable<Recipient> GetAllRecipients(string userName)
        {
            var giftUser = _getUser(userName);

            return _context.Recipients
                .Where(r=> r.GiftUser == giftUser)
                .ToList();
        }

        public void AddRecipient(Recipient newRecipient)
        {
            _context.Recipients.Add(newRecipient);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        private GiftUser _getUser(string userName)
        {
            return _context.GiftUsers
                .Where(gu => gu.UserName == userName)
                .FirstOrDefault();
        }
    }
}