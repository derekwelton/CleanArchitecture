using MediatR;

namespace Application.Modules.Contact.Commands.CreateContact
{
    public class CreateContactCommand: IRequest<int>
    {
        public int Contact_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
    }
}