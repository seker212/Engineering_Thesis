using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerDAL.DatabaseModels
{
    public class Room : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "name", "isArchived" };

        public Room(Guid id, string name, bool isArchived)
        {
            Id = id;
            Name = name;
            IsArchived = isArchived;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsArchived { get; set; }

        public object[] Data => new object[] { Id, Name, IsArchived };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
