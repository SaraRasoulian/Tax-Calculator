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

        [HttpGet("calculate")]
        public ActionResult CalculateTax([FromBody] TaxCalculationDTO taxCalculationDTO)
        {
            var result = _taxService.CalculateTax(taxCalculationDTO);
            return Ok(result);
        }
    }
}
