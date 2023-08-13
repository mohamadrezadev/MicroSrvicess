using ApiGateway.ForWeb.Models.DiscountServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.ForWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _DiscountService;

    public DiscountController(IDiscountService discountService)
    {
        _DiscountService = discountService;
    }
    [HttpGet]
    public IActionResult GetDiscountByCode(string code)
    {
       var result= _DiscountService.GetDiscountByCode(code);
        return Ok(result);
    }
    [HttpGet("{Id}")]
    public IActionResult GetDiscountById(Guid Id)
    {
        var result=_DiscountService.GetDiscountById(Id);
        return Ok(result);
    }
    [HttpPut]
    public IActionResult Put(Guid Id)
    {
        var result=_DiscountService.UseDiscount(Id);
        return Ok(result);
    }
}
