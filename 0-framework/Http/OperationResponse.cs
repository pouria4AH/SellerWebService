using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_framework.Http
{
    public class OperationResponse
    {
        public string Type { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
        public OperationResponse SendStatus(OperationResponseStatusType type, string message, object data)
        {
            return new OperationResponse
            {
                Type = type.ToString(),
                Message = message,
                Data = data
            };
        }
    }
    public enum OperationResponseStatusType
    {
        Success,
        Warning,
        Danger,
        Info
    }
}
