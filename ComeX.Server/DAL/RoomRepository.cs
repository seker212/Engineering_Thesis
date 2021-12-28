using ComeX.Server.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DAL
{
    public class RoomRepository : ObjectRepository<Room>, IRoomRepository
    {
        public RoomRepository(string connectionString) : base(connectionString, "rooms", Room.ColumnNames)
        {
        }

        public Room GetRoom(Guid id) => Query().Where("id", id).First<Room>();
        public IEnumerable<Room> GetRooms() => Query().Get<Room>();
    }
}
