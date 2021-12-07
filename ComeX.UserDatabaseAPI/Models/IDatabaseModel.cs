using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Models
{
    public interface IDatabaseModel
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
