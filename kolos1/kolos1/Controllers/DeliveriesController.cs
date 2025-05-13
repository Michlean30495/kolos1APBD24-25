using kolos1.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolos1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveriesController : Controller
{
    private readonly IKolosalDB _kolosalDB;

    public DeliveriesController(IKolosalDB kolosalDB)
    {
        _kolosalDB = kolosalDB;
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = _kolosalDB.GetDeliveriesById(id);
        return Ok(result);
    }
}