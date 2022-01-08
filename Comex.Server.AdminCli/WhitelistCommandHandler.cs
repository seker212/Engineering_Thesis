using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comex.Server.AdminCli
{
    class WhitelistCommandHandler
    {
        string _connString;
        UserRepository _userRepository;
        Allowed_userRepository _allowedUserRepository;

        public WhitelistCommandHandler(string connectionString)
        {
            _connString = connectionString;
        }

        public void HandleWhitelistCommand(IEnumerable<string> args)
        {
            using( _userRepository = new UserRepository(_connString))
                using(_allowedUserRepository = new Allowed_userRepository(_connString))
                    switch (args.First())
                    {
                        case "add":
                            AddUserCommand(args.Skip(1).Single());
                            break;
                        case "rm":
                            DeleteUserCommand(args.Skip(1).Single());
                            break;
                        default:
                            throw new UnknownCommandException();
                    }
        }

        private void AddUserCommand(string arg)
        {
            var user = _userRepository.Get().Single(x => x.Username == arg);
            _allowedUserRepository.Insert(new Allowed_user(user.Id));
        }

        private void DeleteUserCommand(string arg)
        {
            var user = _userRepository.Get().Single(x => x.Username == arg);
            _allowedUserRepository.Delete(new Allowed_user(user.Id));
        }
    }
}
