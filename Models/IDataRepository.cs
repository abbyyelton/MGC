using System.Collections.Generic;
using System.Threading.Tasks;

namespace MGC.Models
{
    public interface IDataRepository
    {
        IEnumerable<Gift> GetAllGifts(string userName);

        Gift GetGift(int Id);

        void AddGift(Gift gift);
        void DeleteGift(int Id);
        void UpdateGift(Gift gift);
        void UpdateAllGifts(IEnumerable<Gift> gifts);

        Task<bool> SaveChangesAsync();
    }
}