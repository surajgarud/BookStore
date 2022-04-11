using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRL
    {
        public FeedbackModel AddFeedback(FeedbackModel feedback, int userId);
        public string UpdateFeedback(FeedbackModel feedback, int userId, int feedbackId);
        public bool DeleteFeedback(int feedbackId, int userId);
        public List<FeedbackModel> GetRecordsByBookId(int bookId);
    }
}
