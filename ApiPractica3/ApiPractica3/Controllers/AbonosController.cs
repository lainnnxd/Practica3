using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using ApiPractica3.Models;

[ApiController]
[Route("api/[controller]")]
public class AbonosController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AbonosController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("RegistrarAbono")]
    public async Task<IActionResult> RegistrarAbono([FromBody] Abono abono)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id_Compra", abono.Id_Compra);
            parameters.Add("@Monto", abono.Monto);

            try
            {
               
                await connection.ExecuteAsync("RegistrarAbono", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
