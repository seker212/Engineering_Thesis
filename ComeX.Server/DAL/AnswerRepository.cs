using ComeX.Server.DatabaseModels;
using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DAL
{
    public class AnswerRepository : ObjectRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(string connectionString) : base(connectionString, "answers", Answer.ColumnNames)
        {
        }

        public IEnumerable<Answer> GetAnswers(Guid surveyId) => Query().Where("surveyId", surveyId).Get<Answer>();
        public Answer GetUserAnswered(Guid surveyId, Guid userId) => Query().Where("surveyId", surveyId).Where("userId", userId).FirstOrDefault<Answer>();
        public Answer InsertAnswer(Answer ans) => Insert(ans);
    }
}
