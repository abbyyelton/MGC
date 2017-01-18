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
    [Route("api/Recipients")]
    [Authorize]
    public class RecipientsController : Controller
    {
        private ILogger<GiftsController> _logger;
        private IDataRepository _repository;

        public RecipientsController(IDataRepository repository, ILogger<GiftsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllRecipients(User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<RecipientViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Recipients: {ex}");
                return BadRequest("Error occurred.");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]RecipientViewModel theRecipient)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newRecipient = Mapper.Map<Recipient>(theRecipient);
                    newRecipient.GiftUser = _repository.GetGiftUserByName(User.Identity.Name);

                    _repository.AddRecipient(newRecipient);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"api/Recipients/{theRecipient.Name}", Mapper.Map<RecipientViewModel>(theRecipient));
                    }
                    else
                    {
                        _logger.LogError("Failed to save changes to the database");
                        return BadRequest("Failed to save changes to the database");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to create new Recipient: {ex}");
                    return BadRequest($"Failed to create new Recipient: {ex}");
                }
            }
            return BadRequest(ModelState);
        }


    }
}
