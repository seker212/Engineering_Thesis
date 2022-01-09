using ComeX.Server.DatabaseModels;
using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
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