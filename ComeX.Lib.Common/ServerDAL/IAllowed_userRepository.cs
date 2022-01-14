using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using System;

namespace ComeX.Lib.Common.ServerDAL
{
    public interface IAllowed_userRepository : IObjectRepository<Allowed_user>
    {
        Allowed_user GetAllowed_user(Guid userId);
    }
}