using ComeX.Server.DatabaseModels;
using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using System;

namespace ComeX.Server.DAL
{
    public interface IBlocked_userRepository : IObjectRepository<Blocked_user>
    {
        Blocked_user GetBlocked_user(Guid userId);
    }
}