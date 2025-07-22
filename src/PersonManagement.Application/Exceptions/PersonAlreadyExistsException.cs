using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Application.Exceptions
{
    public class PersonAlreadyExistsException(string personalNumber) :
        Exception($"A person with this personal number {personalNumber} already exists")
    {
    }
}
