using MediatR;

namespace Application.Contact.Commands.CreateContact
{
    public class CreateContactCommand: IRequest<int>
    {
        public int Contact_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}