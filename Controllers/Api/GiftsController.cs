using AutoMapper;
using MGC.Models;
using MGC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGC.Controllers.Api
{

    [Route("api/gifts")]
    [Authorize]
    public class GiftsController : Controller
    {
        private ILogger<GiftsController> _logger;
        private IDataRepository _repository;

        public GiftsController(IDataRepository repository, ILogger<GiftsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllGifts(User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<GiftViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all gifts: {ex}");
                return BadRequest("Error occurred.");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]IEnumerable<GiftViewModel> theGifts)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newGift = new Gift();
                    List<Gift> returnGifts = new List<Gift>();
                    foreach (var gift in theGifts)
                    {
                        newGift = Mapper.Map<Gift>(gift);
                        newGift.Holiday = _repository.GetHolidayByName(gift.HolidayName);
                        newGift.Recipient = _repository.GetRecipientByName(gift.RecipientName);
                        newGift.GiftUser = _repository.GetGiftUserByName(User.Identity.Name);

                        _repository.AddGift(newGift);
                        returnGifts.Add(newGift);
                    }

                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"api/gifts/", Mapper.Map<IEnumerable<GiftViewModel>>(returnGifts));
                    }
                    else
                    {
                        _logger.LogError("Failed to save changes to the database");
                        return BadRequest("Failed to save changes to the database");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to create new gift: {ex}");
                    return BadRequest($"Failed to create new gift: {ex}");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("")]
        public async Task<IActionResult> Delete([FromBody]GiftViewModel theGift)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newGift = Mapper.Map<Gift>(theGift);

                    newGift.GiftUser = _repository.GetGiftUserByName(User.Identity.Name);

                    _repository.DeleteGift(newGift);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"api/gifts/{theGift.Name}", Mapper.Map<GiftViewModel>(newGift));
                    }
                    else
                    {
                        _logger.LogError("Failed to save changes to the database");
                        return BadRequest("Failed to save changes to the database");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to delete gift: {ex}");
                    return BadRequest($"Failed to delete gift: {ex}");
                }
            }
            return BadRequest(ModelState);
        }
    }


}
