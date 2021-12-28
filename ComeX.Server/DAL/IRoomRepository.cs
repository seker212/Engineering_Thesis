using ComeX.Server.DatabaseModels;
using System;
using System.Collections.Generic;

namespace ComeX.Server.DAL
{
    public interface IRoomRepository : IObjectRepository<Room>
    {
        Room GetRoom(Guid id);
        IEnumerable<Room> GetRooms();
    }
}