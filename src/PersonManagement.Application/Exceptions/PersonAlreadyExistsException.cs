namespace PersonManagement.Application.Exceptions
{
    public class PersonAlreadyExistsException(string personalNumber) :
        Exception($"A person with this personal number {personalNumber} already exists")
    {
    }
}
