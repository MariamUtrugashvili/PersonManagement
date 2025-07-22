using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Application.Exceptions
{
    public class RelationDoesNotExistException() :
    Exception("This relation does not exists")
    {
    }
}
