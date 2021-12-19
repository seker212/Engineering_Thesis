using ComeX.Server.DatabaseModels;
using System.Collections.Generic;

namespace ComeX.Server.DAL
{
    public interface IUser_to_roomRepository : IPairRepository<User_to_room>
    {
        IEnumerable<User_to_room> GetUserRooms(User user);
    }
}