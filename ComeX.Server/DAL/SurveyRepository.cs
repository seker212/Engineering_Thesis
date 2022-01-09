using ComeX.Server.DatabaseModels;
using ComeX.Lib.Common.ServerDAL;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DAL
{
    public class SurveyRepository : ObjectRepository<Survey>, ISurveyRepository
    {
        public SurveyRepository(string connectionString) : base(connectionString, "surveys", Survey.ColumnNames)
        {
        }

        public Survey GetSurvey(Guid id) => Query().Where("id", id).First<Survey>();
        public IEnumerable<Survey> GetSurveys(Guid roomId, string sendTime) => Query().Where("roomId", roomId).WhereTime("sendTime", "=<", sendTime).Limit(200).Get<Survey>();
        public Survey InsertSurvey(Survey srv) => Query().Insert(GenerateDataDictionary(srv, 0)) == 1 ? srv : throw new Exception();
    }
}
