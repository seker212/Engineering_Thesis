using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Auth
{
    class UserApiException : Exception
    {
        public UserApiException()
        {
        }

        public UserApiException(string message) : base(message)
        {
        }

        public UserApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
