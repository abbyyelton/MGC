using System.Collections.Generic;
using System.Threading.Tasks;

namespace MGC.Models
{
    public interface IDataRepository
    {
        IEnumerable<Gift> GetAllGifts(string userName);

        Gift GetGift(int Id);

        void AddGift(Gift gift);
        void DeleteGift(Gift gift);
        void UpdateGift(Gift gift);
        void UpdateAllGifts(IEnumerable<Gift> gifts);

        Holiday GetHolidayByName(string holidayName);
        Recipient GetRecipientByName(string recipientName);

        GiftUser GetGiftUserByName(string userName);

        IEnumerable<Holiday> GetAllHolidays();  
        void AddHoliday(Holiday newHoliday);

        IEnumerable<Recipient> GetAllRecipients(string userName);
        void AddRecipient(Recipient newHoliday);

        Task<bool> SaveChangesAsync();
    }
}