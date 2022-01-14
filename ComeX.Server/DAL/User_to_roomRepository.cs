using ComeX.Server.DatabaseModels;
using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DAL
{
    public class User_to_roomRepository : PairRepository<User_to_room>, IUser_to_roomRepository
    {
        public User_to_roomRepository(string connectionString) : base(connectionString, "User_to_room", User_to_room.ColumnNames)
        {
        }

        public IEnumerable<User_to_room> GetUserRooms(User user) => Query().Where("id", user.Id).Get<User_to_room>();
    }
}
