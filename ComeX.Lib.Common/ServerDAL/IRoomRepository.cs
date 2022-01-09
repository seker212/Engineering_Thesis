using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using System;
using System.Collections.Generic;

namespace ComeX.Lib.Common.ServerDAL
{
    public interface IRoomRepository : IObjectRepository<Room>
    {
        Room GetRoom(Guid id);
        IEnumerable<Room> GetRooms();
    }
}