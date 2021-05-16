using SW.Item.Data.Common;
using SW.Item.Data.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using SW.Item.Data;

namespace SW.Item.Core.FeedbackManagement
{
    public class FeedbackManagement : IFeedbackManagement
    {
        private ApplicationDbContext _dbContext;


        public FeedbackManagement(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Response AddFeedback(ItemFeedback feedback)
        {
            try
            {
                feedback.AddedTime=DateTime.Now;
                _dbContext.ItemFeedback.Add(feedback);
                _dbContext.SaveChanges();
                return new Response
                {
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }

        public Response DeleteFeedback(int feedbackId)
        {
            try
            {
                ItemFeedback f= _dbContext.ItemFeedback.Find(feedbackId);
                if (f == null)
                    return new Response()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Commentaire non trouvé."
                    };

                _dbContext.ItemFeedback.Remove(f);

                return new Response
                {
                    Status = HttpStatusCode.OK
                };

            }
            catch (Exception e)
            {
                return new Response
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }

    }
}
