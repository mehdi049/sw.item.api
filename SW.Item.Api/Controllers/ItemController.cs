using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SW.Item.Core.ItemManagement;
using SW.Item.Data.Common;
using SW.Item.Data.Models;

namespace SW.Item.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IItemManagement _itemManagement;

        public ItemController(ILogger<ItemController> logger, IItemManagement itemManagement, IWebHostEnvironment environment)
        {
            _logger = logger;
            _itemManagement = itemManagement;
            _environment = environment;
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

        [Route("add")]
        [HttpPost]
        public IActionResult Add(Data.Entities.Item item)
        {
            Response response = _itemManagement.AddItem(item);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return BadRequest(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [Route("uploadItemImages")]
        [HttpPost]
        public IActionResult UploadItemImages([FromForm] IFormFile[] model)
        {
            string uploadPath = _environment.WebRootPath + "\\SW\\upload\\items\\";

            Response response = _itemManagement.UploadItemImages(model, uploadPath);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK, Body = response.Body });

            return BadRequest(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [Route("getItems")]
        public IActionResult GetItems()
        {
            ItemModel[] items = _itemManagement.GetItems();
            return Ok(new Response { Status = HttpStatusCode.OK, Body = items });
        }

        [Route("getItemsByCategory/{id}")]
        public IActionResult GetItemsByCategory(int id)
        {
            ItemModel[] items = _itemManagement.GetItemsByCategory(id);
            return Ok(new Response { Status = HttpStatusCode.OK, Body = items });
        }

        [Route("getItemsBySubCategory/{id}")]
        public IActionResult GetItemsBySubCategory(int id)
        {
            ItemModel[] items = _itemManagement.GetItemsByCategory(id);
            return Ok(new Response { Status = HttpStatusCode.OK, Body = items });
        }

        [Route("getItem/{id}")]
        public IActionResult GetItem(int id)
        {
            try
            {
                ItemModel item = _itemManagement.GetItem(id);
                return Ok(new Response { Status = HttpStatusCode.OK, Body = item });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = "Une erreur s'est produite, veuillez réessayer." });
            }
            
        }

    }
}
