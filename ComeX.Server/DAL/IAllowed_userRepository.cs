using ComeX.Server.DatabaseModels;
using System;

namespace ComeX.Server.DAL
{
    public interface IAllowed_userRepository : IObjectRepository<Allowed_user>
    {
        Allowed_user GetAllowed_user(Guid userId);
    }
}