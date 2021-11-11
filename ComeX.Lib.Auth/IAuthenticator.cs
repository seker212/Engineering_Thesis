using System;

namespace ComeX.Lib.Auth
{
    /// <summary>
    /// Allows to authenticate user based on given credentials.
    /// </summary>
    /// <typeparam name="TCredentials">Class that stores credentials for authentication.</typeparam>
    /// <typeparam name="TUser">Class that represents user that is being authenticated.</typeparam>
    public interface IAuthenticator<TCredentials, TUser> 
                        where TCredentials: class 
                        where TUser : class
    {
        /// <summary>
        /// Checks if user with given <paramref name="credentials"/> exists and is valid.
        /// </summary>
        /// <param name="credentials">Credentials for validating user.</param>
        /// <exception cref="ArgumentNullException">Given <paramref name="credentials"/> object was null.</exception>
        /// <exception cref="InvalidCredentialsException">No user matches given <paramref name="credentials"/></exception>
        /// <returns>Object of a valid user.</returns>
        public TUser ValidateUser(TCredentials credentials);
    }
}
