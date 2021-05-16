using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SW.Item.Core.FeedbackManagement;
using SW.Item.Data.Common;
using SW.Item.Data.Entities;


namespace SW.Item.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<FeedbackController> _logger;
        private readonly IFeedbackManagement _feedbackManagement;

        public FeedbackController(ILogger<FeedbackController> logger, IFeedbackManagement feedbackManagement)
        {
            _logger = logger;
            _feedbackManagement = feedbackManagement;
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add(ItemFeedback feedback)
        {
            Response response = _feedbackManagement.AddFeedback(feedback);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [Route("delete")]
        [HttpDelete]
        public IActionResult Delete(int feedbackId)
        {
            Response response = _feedbackManagement.DeleteFeedback(feedbackId);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return Ok(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }
    }
}
