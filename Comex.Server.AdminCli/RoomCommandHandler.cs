using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comex.Server.AdminCli
{
    public class RoomCommandHandler
    {
        string _connString;
        RoomRepository _repository;

        public RoomCommandHandler(string connectionString)
        {
            _connString = connectionString;
        }

        public void HandleRoomCommand(IEnumerable<string> args)
        {
            using (_repository = new RoomRepository(_connString))
                switch (args.First())
                {
                    case "mk":
                        CreateRoomCommand(args.Skip(1).Single());
                        break;
                    case "ar":
                        ArchiveRoomCommand(args.Skip(1).Single());
                        break;
                    case "unar":
                        UnarchiveRoomCommand(args.Skip(1).Single());
                        break;
                    default:
                        throw new UnknownCommandException();
                }
        }

        void CreateRoomCommand(string arg)
        {
            _repository.Insert(new Room(Guid.NewGuid(), arg, false));
        }

        void ArchiveRoomCommand(string arg)
        {
            var room = _repository.GetRooms().Single(x => x.Name == arg);
            room.IsArchived = true;
            _repository.Update(room);
        }

        void UnarchiveRoomCommand(string arg) {
            var room = _repository.GetRooms().Single(x => x.Name == arg);
            room.IsArchived = false;
            _repository.Update(room);
        }
    }
}
