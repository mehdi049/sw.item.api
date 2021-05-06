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

            return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add(Data.Entities.Item item)
        {
            Response response = _itemManagement.AddItem(item);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [Route("uploadItemImages")]
        [HttpPost]
        public IActionResult UploadItemImages([FromForm] IFormFile[] model)
        {
            string uploadPath = _environment.WebRootPath + "\\SW\\upload\\items\\";

            Response response = _itemManagement.UploadItemImages(model, uploadPath);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK, Body = response.Body });

            return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [Route("getItems")]
        public IActionResult GetItems()
        {
            try
            {
                ItemModel[] items = _itemManagement.GetItems();
                return Ok(new Response { Status = HttpStatusCode.OK, Body = items });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = "Une erreur s'est produite, veuillez réessayer." });
            }

        }

        [Route("getItemsByCategory/{id}")]
        public IActionResult GetItemsByCategory(int id)
        {
            try
            {
                ItemModel[] items = _itemManagement.GetItemsByCategory(id);
                return Ok(new Response { Status = HttpStatusCode.OK, Body = items });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = "Une erreur s'est produite, veuillez réessayer." });
            }

        }

        [Route("getItemsByUser/{id}")]
        public IActionResult GetItemsByUser(int id)
        {
            try
            {
                ItemModel[] items = _itemManagement.GetItemsByUser(id);
                return Ok(new Response { Status = HttpStatusCode.OK, Body = items });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = "Une erreur s'est produite, veuillez réessayer." });
            }
        }

        [Route("getItemsBySubCategory/{id}")]
        public IActionResult GetItemsBySubCategory(int id)
        {
            try
            {
                ItemModel[] items = _itemManagement.GetItemsByCategory(id);
                return Ok(new Response { Status = HttpStatusCode.OK, Body = items });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = "Une erreur s'est produite, veuillez réessayer." });
            }

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

        [HttpDelete]
        [Route("deleteItem/{itemId}/{userId}")]
        public IActionResult DeleteItemByItemUserId(int itemId, int userId)
        {
            string uploadPath = _environment.WebRootPath + "\\SW\\upload\\items\\";

            Response response = _itemManagement.DeleteItemByItemUserId(itemId, userId, uploadPath);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [HttpDelete]
        [Route("deleteItem/{itemId}")]
        public IActionResult DeleteItemByItemId(int itemId)
        {
            string uploadPath = _environment.WebRootPath + "\\SW\\upload\\items\\";

            Response response = _itemManagement.DeleteItemByItemId(itemId, uploadPath);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

    }
}
