using ComeX.Server.DatabaseModels;
using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using System;
using System.Collections.Generic;

namespace ComeX.Server.DAL
{
    public interface ISurveyRepository : IObjectRepository<Survey>
    {
        Survey GetSurvey(Guid id);
        IEnumerable<Survey> GetSurveys(Guid roomId, string sendTime);
        public Survey InsertSurvey(Survey srv);
    }
}