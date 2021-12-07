namespace ComeX.UserDatabaseAPI.Models
{
    public interface IUserDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string TokensCollectionName { get; set; }
        string UsersCollectionName { get; set; }
    }
}