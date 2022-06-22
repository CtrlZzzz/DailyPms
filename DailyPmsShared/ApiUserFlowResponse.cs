using System.Text.Json.Serialization;

namespace DailyPmsShared
{
    public class ApiUserFlowResponse
    {
        public const string APIVERSION = "1.0.0";
        public const string CONTINUATION = "Continue";
        public const string VALIDATION_ERROR = "ValidationError";

        public ApiUserFlowResponse()
        {
            Version = APIVERSION;
            Action = CONTINUATION;
        }

        public ApiUserFlowResponse(string action, string userMessage)
        {
            Version = APIVERSION;
            Action = action;
            UserMessage = userMessage;

            if (action == VALIDATION_ERROR)
            {
                Status = "400";
            }
        }

        public string Version { get; set; }

        public string Action { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string UserMessage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Status { get; set; }
    }
}

