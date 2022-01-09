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
                    case "rm":
                        DeleteRoomCommand(args.Skip(1).Single());
                        break;
                    case "ar":
                        ArchiveRoomCommand(args.Skip(1).Single());
                        break;
                    default:
                        throw new UnknownCommandException();
                }
        }

        void CreateRoomCommand(string arg)
        {
            _repository.Insert(new Room(Guid.NewGuid(), arg, false));
        }

        void DeleteRoomCommand(string arg)
        {
            _repository.Delete(_repository.GetRooms().Single(x => x.Name == arg));
        }

        void ArchiveRoomCommand(string arg)
        {
            var room = _repository.GetRooms().Single(x => x.Name == arg);
            room.IsArchived = true;
            _repository.Update(room);
        }
    }
}
