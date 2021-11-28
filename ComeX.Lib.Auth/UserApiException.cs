using System;
using System.Net;

namespace ComeX.Lib.Auth
{
    /// <summary>
    /// Exception thrown by <see cref="UserApiManager"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3925:\"ISerializable\" should be implemented correctly")]
    public class UserApiException : Exception
    {
        /// <summary>
        /// Status code got from http response.
        /// </summary>
        public HttpStatusCode? ResponseStatusCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserApiException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UserApiException(string message) : base(message)
        {
            ResponseStatusCode = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserApiException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="responseStatusCode">Status code got from http response.</param>
        public UserApiException(string message, HttpStatusCode? responseStatusCode)
        {
            ResponseStatusCode = responseStatusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserApiException"/> class with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public UserApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
