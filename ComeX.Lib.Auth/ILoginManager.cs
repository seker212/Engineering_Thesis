using System.Threading.Tasks;

namespace ComeX.Lib.Auth
{
    public interface ILoginManager
    {
        /// <summary>
        /// Tries to login user based on <paramref name="token"/>.
        /// </summary>
        /// <param name="connectionId">Connection Id used to send <paramref name="token"/>.</param>
        /// <param name="token">Token value.</param>
        /// <exception cref="InvalidCredentialsException">Invalid token.</exception>
        void Login(string connectionId, string token);

        /// <summary>
        /// Logs out a connection.
        /// </summary>
        /// <remarks>
        /// If a user uses more than one connection only the given connection will be removed.
        /// </remarks>
        /// <param name="connectionId">Id of the terminated connection.</param>
        Task Logout(string connectionId);
    }
}