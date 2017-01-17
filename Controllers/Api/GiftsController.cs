﻿using AutoMapper;
using MGC.Models;
using MGC.ViewModels;
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
                var results = _repository.GetAllGifts(this.User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<GiftViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all gifts: {ex}");
                return BadRequest("Error occurred.");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]GiftViewModel theGift)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newGift = Mapper.Map<Gift>(theGift);
                    newGift.Holiday = new Holiday()
                    {
                        Name = theGift.HolidayName
                    };
                    newGift.Recipient = new Recipient()
                    {
                        Name = theGift.RecipientName
                    };  
                   
               

                   // newGift.UserName = User.Identity.Name;

                    _repository.AddGift(newGift);

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
                    _logger.LogError($"Failed to create new gift: {ex}");
                    return BadRequest($"Failed to create new gift: {ex}");
                }
            }
            return BadRequest(ModelState);
        }

       // [HttpDelete("")]
        //public async Task<IActionResult> Delete([FromBody]GiftViewModel theGift)
        //{
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        var newGift = Mapper.Map<Gift>(theGift);
                   
            //        // newGift.UserName = User.Identity.Name;

            //        _repository.DeleteGift(newGift.Id);

            //        if (await _repository.SaveChangesAsync())
            //        {
            //            return Created($"api/gifts/{theGift.Name}", Mapper.Map<GiftViewModel>(newGift));
            //        }
            //        else
            //        {
            //            _logger.LogError("Failed to save changes to the database");
            //            return BadRequest("Failed to save changes to the database");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError($"Failed to create new gift: {ex}");
            //        return BadRequest($"Failed to create new gift: {ex}");
            //    }
            //}
            //return BadRequest(ModelState);
       // }
    }


}