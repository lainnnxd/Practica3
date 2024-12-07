using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;


[ApiController]
[Route("api/[controller]")]

public class ComprasController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public ComprasController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("Pendientes")]
    public async Task<IActionResult> GetComprasPendientes()
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var result = await connection.QueryAsync("ObtenerComprasPendientes", commandType: CommandType.StoredProcedure);
            return Ok(result);
        }
    }
}
