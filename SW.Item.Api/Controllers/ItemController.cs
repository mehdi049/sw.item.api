using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SW.Item.Core.ItemManagement;
using SW.Item.Data.Common;

namespace SW.Item.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemManagement _itemManagement;

        public ItemController(ILogger<ItemController> logger, IItemManagement itemManagement)
        {
            _logger = logger;
            _itemManagement = itemManagement;
        }

        [Route("batchAddItem")]
        [HttpPost]
        public IActionResult BatchAddItem()
        {
            Response response = _itemManagement.BatchAddItem();
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });
                
            return BadRequest(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [Route("getItems")]
        public IActionResult GetItems()
        {
            Data.Entities.Item[] items = _itemManagement.GetItems();
            return Ok(new Response { Status = HttpStatusCode.OK, Body = items });
        }

        [Route("getItem/{id}")]
        public IActionResult GetItem(int id)
        {
            Data.Entities.Item item = _itemManagement.GetItem(id);
            return Ok(new Response { Status = HttpStatusCode.OK, Body = item });
        }

    }
}
