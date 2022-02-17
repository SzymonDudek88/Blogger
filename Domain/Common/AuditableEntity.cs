using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }
//        Since DateTime is a struct, not a class, you get a DateTime object, not a reference, when you declare a field or variable of that type.

//And, in the same way as an int cannot be null, so this DateTime object can never be null, because it's not a reference.

//Adding the question mark turns it into a nullable type, which means that either it is a DateTime object, or it is null.

//DateTime? is syntactic sugar for Nullable<DateTime>, where Nullable is itself a struct.
        public DateTime? LastModified { get; set; } //zeby mogl byc nullem
        public string LastModifiedBy { get; set;  }

    }  //Created  CreatedBy LastModified LastModifiedBy
}
