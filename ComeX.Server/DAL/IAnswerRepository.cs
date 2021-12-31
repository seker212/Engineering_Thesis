using ComeX.Server.DatabaseModels;
using System;
using System.Collections.Generic;

namespace ComeX.Server.DAL
{
    public interface IAnswerRepository : IObjectRepository<Answer>
    {
        IEnumerable<Answer> GetAnswers(Guid surveyId);
        public Answer InsertAnswer(Answer ans);
    }
}