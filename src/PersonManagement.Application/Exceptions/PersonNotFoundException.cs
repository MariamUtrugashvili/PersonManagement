namespace PersonManagement.Application.Exceptions
{
    public class PersonNotFoundException(int id) : 
        Exception($"Person with Id '{id}' was not found.")
    {
    }

}
