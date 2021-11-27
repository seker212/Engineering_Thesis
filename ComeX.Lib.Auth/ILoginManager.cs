namespace ComeX.Lib.Auth
{
    public interface ILoginManager
    {
        void Login(string connectionId, string token);
        void Logout(string connectionId);
    }
}