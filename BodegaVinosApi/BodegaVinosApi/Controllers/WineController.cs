using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BodegaVinosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineController : ControllerBase
    {
        private readonly IWineService _wineService;

        public WineController(IWineService wineService)
        {
            _wineService = wineService;
        }

        [HttpPost("add")]
        public IActionResult AddWine([FromBody] WineDto wineDto)
        {

            var wineId = _wineService.AddWine(wineDto);

            return Ok(new { WineId = wineId });
        }

        [HttpGet]
        public IActionResult GetAllWines()
        {
            var wines = _wineService.GetAllWines();
            return Ok(wines);
        }

        [HttpGet("{name}")]
        public IActionResult GetWineByName(string name)
        {
            var wine = _wineService.GetWineByName(name);

            if (wine == null)
            {
                return NotFound(new { Message = "Vino no encontrado." });
            }

            return Ok(wine);
        }

        [HttpPut("{id}/stock")]
        public IActionResult UpdateStock(int id, [FromBody] int newStock)
        {
            var success = _wineService.UpdateStock(id, newStock);

            if (!success)
            {
                return NotFound(new { Message = "No se pudo actualizar el stock. Vino no encontrado." });
            }

            return Ok(new { Message = "Stock actualizado con éxito." });
        }
    }
}