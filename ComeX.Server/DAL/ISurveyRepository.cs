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
        IEnumerable<Survey> GetSurveys(Guid roomId, DateTime sendTime);
        public IEnumerable<Survey> GetSurveys(Guid roomId, DateTime sendTime, DateTime endTime);
        public Survey InsertSurvey(Survey srv);
    }
}