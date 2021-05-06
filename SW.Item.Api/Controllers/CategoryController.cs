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
            try
            {
                Response response = _categoryManagement.BatchAddCategory();
                if (response.Status == HttpStatusCode.OK)
                    return Ok(new Response {Status = HttpStatusCode.OK});
                return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = "Une erreur s'est produite, veuillez réessayer." });
            }
        }

        [Route("batchAddSubCategory")]
        [HttpPost]
        public IActionResult BatchAddSubCategory()
        {
            try
            {
                Response response = _categoryManagement.BatchAddSubCategory();
                if (response.Status == HttpStatusCode.OK)
                    return Ok(new Response { Status = HttpStatusCode.OK });

                return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = "Une erreur s'est produite, veuillez réessayer." });
            }
            
        }

        [Route("getCategories")]
        public IActionResult GetCategories()
        {
            try
            {
                Category[] categories = _categoryManagement.GetCategories();
                return Ok(new Response { Status = HttpStatusCode.OK, Body = categories });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = "Une erreur s'est produite, veuillez réessayer." });
            }
            
        }

        [Route("getCategory/{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                Category category = _categoryManagement.GetCategory(id);
                return Ok(new Response { Status = HttpStatusCode.OK, Body = category });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = "Une erreur s'est produite, veuillez réessayer." });
            }
            
        }

    }
}
