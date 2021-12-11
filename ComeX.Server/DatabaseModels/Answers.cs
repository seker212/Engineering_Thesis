﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DatabaseModels
{
    public class Answers : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "content", "surveyId" };

        public Answers(Guid id, string content, Guid surveyId)
        {
            Id = id;
            Content = content;
            SurveyId = surveyId;
        }

        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid SurveyId { get; set; }

        public object[] Data => new object[] { Id, Content, SurveyId };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
