using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Auth
{
    /// <summary>
    /// Exception thrown by authenticator when provided credentials don't match any user.
    /// </summary>
    public class InvalidCredentialsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCredentialsException"/> class with default message.
        /// </summary>
        public InvalidCredentialsException() : base("Provided credentials were invalid.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCredentialsException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidCredentialsException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCredentialsException"/> class with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public InvalidCredentialsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
