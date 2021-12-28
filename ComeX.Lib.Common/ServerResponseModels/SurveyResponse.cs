﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class SurveyResponse {
        public SurveyResponse(string username, Guid roomId, string question, bool isMultipleChoice, Dictionary<SurveyAnswerResponse, int> answerList) {
            Username = username;
            RoomId = roomId;
            Question = question;
            IsMultipleChoice = isMultipleChoice;
            AnswerList = answerList;
        }

        public SurveyResponse() { }

        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("Question")]
        public string Question { get; set; }
        [JsonProperty("IsMultipleChoice")]
        public bool IsMultipleChoice { get; set; }
        [JsonProperty("AnswerList")]
        public Dictionary<SurveyAnswerResponse, int> AnswerList { get; set; }
    }
}