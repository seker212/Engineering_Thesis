namespace ComeX.Lib.Auth
{
    public interface ICredentials
    {
        /// <summary>
        /// Checks if the token is currently valid.
        /// </summary>
        /// <returns>True if token is valid, false otherwise.</returns>
        bool IsValid();
    }
}
