using ComeX.UserDatabaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.DAL
{
    public interface IRepository<T> where T : IDatabaseModel
    {

    }
}
