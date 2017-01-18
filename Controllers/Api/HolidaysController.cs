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
    [Route("api/holidays")]
    [Authorize]
    public class HolidaysController : Controller
    {
        private ILogger<GiftsController> _logger;
        private IDataRepository _repository;

        public HolidaysController(IDataRepository repository, ILogger<GiftsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllHolidays();
                return Ok(Mapper.Map<IEnumerable<HolidayViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all holidays: {ex}");
                return BadRequest("Error occurred.");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]HolidayViewModel theHoliday)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newHoliday = Mapper.Map<Holiday>(theHoliday);

                    _repository.AddHoliday(newHoliday);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"api/holidays/{theHoliday.Name}", Mapper.Map<HolidayViewModel>(theHoliday));
                    }
                    else
                    {
                        _logger.LogError("Failed to save changes to the database");
                        return BadRequest("Failed to save changes to the database");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to create new holiday: {ex}");
                    return BadRequest($"Failed to create new holiday: {ex}");
                }
            }
            return BadRequest(ModelState);
        }


    }
}
