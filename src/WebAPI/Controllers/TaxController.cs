using CongestionTaxCalculator.DTOs;
using CongestionTaxCalculator.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/tax")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly TaxService _taxService;
        public TaxController()
        {
            _taxService = new TaxService();
        }

        /// <summary>
        /// Calculate the total toll fee
        /// </summary>
        /// <param name="taxCalculationDTO">
        /// The DTO has following parameters:
        /// VehicleType - the vehicle
        /// PassesDates - date and time of all passes
        /// </param>
        /// <returns>the total congestion tax for that period of time</returns>
        [HttpGet("calculate")]
        public ActionResult CalculateTax([FromBody] TaxCalculationDTO taxCalculationDTO)
        {
            var result = _taxService.CalculateTax(taxCalculationDTO);
            return Ok(result);
        }
    }
}
