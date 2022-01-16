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
            var users = _userRepository.Get();
            if (users.Any(x => x.Username == arg)){
                var user = users.Single(x => x.Username == arg);
                if (user.IsTemp)
                    throw new Exception("User already on the whitelist");
                else
                {
                    var al = _allowedUserRepository.GetAllowed_user(user.Id);
                    if (al != null)
                        throw new Exception("User already on the whitelist");
                    _allowedUserRepository.Insert(new Allowed_user(user.Id));
                }
            }
            else
                _userRepository.Insert(new User(Guid.Empty, arg, true));
        }

        private void DeleteUserCommand(string arg)
        {
            var users = _userRepository.Get();
            if (users.Any(x => x.Username == arg))
            {
                var user = users.Single(x => x.Username == arg);
                if (user.IsTemp)
                    _userRepository.Delete(user);
                else
                {
                    var allowedUsers = _allowedUserRepository.Get();
                    if (allowedUsers.Any(x => x.UserId == user.Id))
                        _allowedUserRepository.Delete(new Allowed_user(user.Id));
                    else
                        throw new Exception("User already removed from whitelist");
                }
            }
            else
                throw new Exception("User not found");
        }
    }
}
