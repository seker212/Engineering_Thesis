using ComeX.Server.DatabaseModels;
using System;

namespace ComeX.Server.DAL
{
    public interface IRoomRepository : IObjectRepository<Room>
    {
        Room GetRoom(Guid id);
    }
}