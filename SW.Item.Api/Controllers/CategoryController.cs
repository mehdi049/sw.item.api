using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SW.Item.Core.CategoryManagement;
using SW.Item.Data.Common;
using SW.Item.Data.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SW.Item.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryManagement _categoryManagement;

        public CategoryController(ILogger<CategoryController> logger, ICategoryManagement categoryManagement)
        {
            _logger = logger;
            _categoryManagement = categoryManagement;
        }

        [Route("batchAddCategory")]
        [HttpPost]
        public IActionResult BatchAddCategory()
        {
            Response response = _categoryManagement.BatchAddCategory();
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return BadRequest(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [Route("batchAddSubCategory")]
        [HttpPost]
        public IActionResult BatchAddSubCategory()
        {
            Response response = _categoryManagement.BatchAddSubCategory();
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return BadRequest(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [Route("getAllCategories")]
        public IActionResult GetAllCategories()
        {
            Category[] categories = _categoryManagement.GetAllCategories();
            return Ok(new Response { Status = HttpStatusCode.OK, Body = categories });
        }
    }
}
