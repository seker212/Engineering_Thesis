using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerDAL
{
    public class RoomRepository : ObjectRepository<Room>, IRoomRepository
    {
        public RoomRepository(string connectionString) : base(connectionString, "rooms", Room.ColumnNames)
        {
        }

        public Room GetRoom(Guid id) => Get(id);
        public IEnumerable<Room> GetRooms() => Query().Get<Room>();
    }
}
