namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonResponse 
    {
        public int Id { get; set; }
        public CreatePersonResponse(int id)
        {
            Id = id;
        }
    }
}