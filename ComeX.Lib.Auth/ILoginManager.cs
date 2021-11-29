namespace ComeX.Lib.Auth
{
    public interface ILoginManager
    {
        /// <summary>
        /// Tries to login user based on <paramref name="token"/> and assigns a <paramref name="connectionId"/> as a key to the <see cref="IConnectionCache"/> and <see cref="TokenData"/> as a value.
        /// </summary>
        /// <param name="connectionId">Connection Id used to send <paramref name="token"/>.</param>
        /// <param name="token">Token value.</param>
        /// <exception cref="InvalidCredentialsException">Invalid token.</exception>
        void Login(string connectionId, string token);

        /// <summary>
        /// Logs out a connection. Essentially removes <paramref name="connectionId"/> from <see cref="IConnectionCache"/>.
        /// </summary>
        /// <remarks>
        /// If a user uses more than one connection only the given connection will be removed.
        /// </remarks>
        /// <param name="connectionId">Id of the terminated connection.</param>
        void Logout(string connectionId);
    }
}