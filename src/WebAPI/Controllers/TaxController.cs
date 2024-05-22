using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/tax")]
[ApiController]
public class TaxController : ControllerBase
{
    private readonly ITaxService _taxService;
    public TaxController(ITaxService taxService)
    {
        _taxService = taxService;
    }

    [HttpPost("calculate")]
    public async Task<ActionResult> CalculateTax([FromBody] TaxCalculationDto taxCalculationDto)
    {          
        var result = await _taxService.CalculateTax(taxCalculationDto);
        if(result == -1) return BadRequest();
        return Ok(result);
    }
}
