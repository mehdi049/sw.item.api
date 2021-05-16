using System;
using System.Collections.Generic;
using System.Text;
using SW.Item.Data.Common;
using SW.Item.Data.Entities;

namespace SW.Item.Core.FeedbackManagement
{
    public interface IFeedbackManagement
    {
        Response AddFeedback(ItemFeedback feedback);
        Response DeleteFeedback(int feedbackId);
    }
}
