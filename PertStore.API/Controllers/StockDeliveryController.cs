using Microsoft.AspNetCore.Mvc;
using PetStore.Shared.DTO;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetStore.API.Controllers
{
    [Route("api/[controller]")]
    public class StockDeliveryController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> Post(StockDeliveryCreate stockDeliveryCreate)
        {
            return Ok();
        }
    }
}
